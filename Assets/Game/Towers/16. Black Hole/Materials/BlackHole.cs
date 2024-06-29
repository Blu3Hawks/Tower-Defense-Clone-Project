using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlackHole : MonoBehaviour
{
    public float pullStrength;
    public float scalingFactor;
    public float pullRadius; // Radius within which enemies are affected and pulled in
    public float destructionRadius;
    private Vector3 initialDirection;
    [SerializeField] float speed;
    public float pullForceModifier;
    private List<Enemy> enemiesInPullRadius = new List<Enemy>();

    //properties for when destroying enemies
    [SerializeField] int maxDestroyedEnemes;
    [SerializeField] private int amountOfEnemiesToDestroy;
    [SerializeField] float decreasedBlackHoleSize;
    void Start()
    {
        SettingColliders();
        amountOfEnemiesToDestroy = maxDestroyedEnemes;
    }

    private void Update()
    {
        transform.Translate(initialDirection * Time.deltaTime * speed, Space.World);

        foreach (Enemy enemy in enemiesInPullRadius)
        {
            PullEnemiesInRange(enemy);
        }
    }

    private void SettingColliders()
    {

        SphereCollider sphereCollider = this.GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = pullRadius;

        CapsuleCollider capsuleCollider = this.GetComponentInChildren<CapsuleCollider>();
        capsuleCollider.isTrigger = true;
        capsuleCollider.radius = destructionRadius;
    }

    public void InitializeBlackHole(Vector3 direction, float speed)
    {
        this.initialDirection = direction;
        this.speed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                enemy.navMeshAgent.enabled = false;
            }
            enemiesInPullRadius.Add(enemy);
            Debug.Log(enemy.name + " has been added to the enemies in range list");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.enabled = true;
                agent.SetDestination(GameObject.Find("EndPoint").transform.position);
            }
            enemiesInPullRadius.Remove(enemy);
            Debug.Log("The following enemy has left the range list: " + enemy.name);
        }
    }

    private void PullEnemiesInRange(Enemy enemy)
    {
        if (enemy == null) return;

        Vector3 direction = (transform.position - enemy.transform.position).normalized;
        float distance = Vector3.Distance(transform.position, enemy.transform.position);

        float pullForce = Mathf.Lerp(pullForceModifier, pullStrength, (pullRadius - distance) / pullRadius);

        enemy.transform.position += direction * pullForce * Time.deltaTime;
        enemy.transform.localScale = Vector3.Lerp(enemy.transform.localScale, Vector3.zero, scalingFactor * Time.deltaTime);

        if (distance < destructionRadius)
        {
            Destroy(enemy.gameObject);
            amountOfEnemiesToDestroy--;
            OnDestroyingEnemies();
        }
    }

    private void OnDestroyingEnemies()
    {
        if (amountOfEnemiesToDestroy > 0)
        {
            this.transform.localScale *= (1f - decreasedBlackHoleSize);
            pullStrength *= (1f - decreasedBlackHoleSize);
            destructionRadius *= (1f - decreasedBlackHoleSize);
        }
        if (amountOfEnemiesToDestroy <= 0)
        {
            Debug.Log("This black hole as eaten enough");
            ClearEnemyList(); 
            Destroy(this.gameObject);
        }
    }

    private void ClearEnemyList()
    {
        foreach (Enemy enemy in enemiesInPullRadius)
        {
            enemy.navMeshAgent.enabled = true;
        }
        enemiesInPullRadius.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pullRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, destructionRadius);
    }
}