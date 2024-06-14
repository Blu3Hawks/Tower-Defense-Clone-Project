using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour // the very first and basic enemy script
{
    Enemy enemy;

    [Header("Speed")]
    public float startingSpeed;
    public float currentSpeed;

    [Header("Health Attributes")]
    [SerializeField] public float maxHealth = 6;
    [SerializeField] public float currentHealth;
    public int healthPenalty = 1;
    [SerializeField] public float armor;

    [Header("Rewards")]
    [SerializeField] int goldReward;
    [SerializeField] int gemReward = 2;

    Resources resources;

    [Header("Effects - Slow")]
    //slow effects
    public int amountOfSlowEffects;
    public int maxSlowEffectsAmount = 2;
    public int currentSlowEffectsAmount = 0;
    //slow percentages
    public float maxSlowSpeedPercent = 0.8f;
    public float totalCurrentSlowPercent;
    public List<Tower> towersWithSlowEffect = new List<Tower>();
    //slow timers
    public int slowTimerCounter = 0;
    //movements
    [SerializeField] private Transform endPoint;
    public NavMeshAgent navMeshAgent;
    //private info and setups
    private Rigidbody rb;

    [Header("Buffs from enemies")]
    public bool isMoveSpeedBuffed = false;


    protected virtual void Start()
    {
        SettingUpEnemy();
    }

    protected virtual void SettingUpEnemy()
    {
        //setting things up
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        resources = FindObjectOfType<Resources>();
        SetDestination();
        currentHealth = maxHealth;
        SettingRewards();
        //starting point
        //speed
        startingSpeed = navMeshAgent.speed;
        navMeshAgent.speed = startingSpeed;
    }

    public void SetDestination()
    {
        endPoint = GameObject.Find("EndPoint").transform;
    }
    void OnEnable()
    {
        currentSlowEffectsAmount = 0;
        totalCurrentSlowPercent = 0;
        currentSpeed = startingSpeed;
        currentHealth = maxHealth;
        towersWithSlowEffect.Clear();
    }
    protected virtual void Update()
    {
        currentSpeed = navMeshAgent.speed;
        if (navMeshAgent.enabled == true)
        {
            navMeshAgent.destination = endPoint.position;
        }
    }
    public void SettingRewards()
    {
        DataFiles dataFiles = FindObjectOfType<DataFiles>();
        goldReward = 1; //+ dataFiles.ScrapBuilding.Building_Stats
        Debug.Log("Stuff has been set");
    }
    public void RewardResources()
    {
        if (resources == null) { return; }
        resources.Deposit(goldReward, gemReward);
    }
    public void HealthPenalty()
    {
        if (resources == null) { return; }
        resources.HealthPenalty(healthPenalty);
    }
    private void OnCollisionEnter(Collision collision)
    {
        EndPoint endPointScript = collision.gameObject.GetComponent<EndPoint>();
        if (endPointScript != null)
        {
            DestroyEnemy();
        }
    }

    /*private void HandleCannonEffects2(Tower tower, float damageTaken, GameObject other)
    {
        ProcessHit(damageTaken);
        //Finds the hit point at the enemy
        ParticleSystem particleSystem = other.GetComponent<ParticleSystem>();
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        particleSystem.GetCollisionEvents(gameObject, collisionEvents);
        Vector3 collisionHitPoint = collisionEvents[0].intersection;

        //Finds all of the enemies in the radius around he hit enemy
        Collider[] hitColliders = Physics.OverlapSphere(collisionHitPoint, tower.aoeRadius);
        foreach (Collider collider in hitColliders)
        {
            Enemy enemyInRange = collider.GetComponent<Enemy>();
            if (enemyInRange != null && enemyInRange != this)
            {
                enemyInRange.ProcessHit(damageTaken);
            }
        }
    }
   */

    public virtual void ProcessHit(float damageTaken)
    {
        currentHealth -= damageTaken; // - armor; // if the enemy has 3 armor it will reduce 3 damage on every turn. Can also be with percentages.
        if (currentHealth <= 0)
        {
            DestroyEnemy();
            enemy.RewardResources();
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
