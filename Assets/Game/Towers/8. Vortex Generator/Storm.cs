using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    private float stormDuration;
    private float stormTickDuration;
    private float stormTickDamage;
    private float stormPullStrength;
    public LayerMask enemyLayer;
    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        StartCoroutine(StormRoutine());
    }

    public void Initialize(float duration, float tickDamage, float tickDuration, float pullStrength)
    {
        stormDuration = duration;
        stormTickDamage = tickDamage;
        stormTickDuration = tickDuration;
        stormPullStrength = pullStrength;
    }

    private IEnumerator StormRoutine()
    {
        float endTime = Time.time + stormDuration;
        while (Time.time < endTime)
        {
            PullAndDamageEnemies();
            yield return new WaitForSeconds(stormTickDuration);
        }
        Destroy(gameObject);
    }

    private void PullAndDamageEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, ps.shape.radius, enemyLayer);
        foreach (Collider collider in colliders)
        {
            Enemy enemyInSphere = collider.GetComponent<Enemy>();
            if (enemyInSphere != null)
            {
                enemyInSphere.ProcessHit(stormTickDamage);
                float distanceToCenter = Vector3.Distance(transform.position, enemyInSphere.transform.position);

                Vector3 directionToCenter = (transform.position - enemyInSphere.transform.position).normalized;
                float pullStrength = CalculatePullStrength(distanceToCenter, ps.shape.radius);

                ApplyPullForce(directionToCenter, pullStrength, enemyInSphere);
            }
        }
    }

    public void ApplyPullForce(Vector3 forceDir, float forceStrength, Enemy enemy)
    {
        StartCoroutine(PullForceRoutine(forceDir, forceStrength, enemy));
    }

    private IEnumerator PullForceRoutine(Vector3 forceDir, float forceStrength, Enemy enemy)
    {
        Rigidbody enemyRB = enemy.GetComponent<Rigidbody>();
        if (enemyRB != null)
        {
            enemy.navMeshAgent.enabled = false;
            enemyRB.isKinematic = false;
            enemyRB.AddForce(forceDir * forceStrength, ForceMode.Impulse);
            Debug.Log(forceDir);
            Debug.Log(forceStrength);
        }

        yield return new WaitForSeconds(0.1f); // wait time is 0.2 so the push effect on every tick will work properly
        if (enemyRB != null)
        {
            enemyRB.isKinematic = true;
            enemy.navMeshAgent.enabled = true;
        }
    }
    private float CalculatePullStrength(float distance, float maxDistance)
    {
        return Mathf.Lerp(stormPullStrength, 0, distance / maxDistance);
    }
}
