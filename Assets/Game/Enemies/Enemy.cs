using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
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
    private void OnParticleCollision(GameObject other)
    {
        Tower tower = other.GetComponentInParent<Tower>();
        float damageTaken = tower.damageAmountPerParticle;

        if (tower != null)
        {
            switch (tower.towerType)
            {
                case Tower.TowerTypes.Slowing:
                    break;
                case Tower.TowerTypes.Basic1:
                    ProcessHit(damageTaken);
                    break;
                case Tower.TowerTypes.Cannon2:
                    break;
                case Tower.TowerTypes.NaniteSwarmLauncher3:
                    NaniteSwarmLauncherTower naniteTower = tower as NaniteSwarmLauncherTower;
                    if (naniteTower != null)
                    {
                        naniteTower.HandleNaniteSwarmLauncher(this);
                    }
                    break;
                case Tower.TowerTypes.FungalSporeLauncher4:
                    FungalSporeLauncher fungalTower = tower as FungalSporeLauncher;
                    if (fungalTower != null)
                    {
                        fungalTower.HandleFungalSporeLauncher(this);
                    }
                    break;
                case Tower.TowerTypes.QuantumEntaglement5:
                    QuantumEntaglementTower quantumEntaglement = tower as QuantumEntaglementTower;
                    if (quantumEntaglement != null)
                    {
                        quantumEntaglement.HandleQuantumEntaglement5();
                    }
                    break;
                case Tower.TowerTypes.SonicResonanceCannon6:
                    SonicResonanceCannonTower sonicResonanceCannon = tower as SonicResonanceCannonTower;
                    if (sonicResonanceCannon != null)
                    {
                        Vector3 direction = -transform.forward;
                        sonicResonanceCannon.HandleSonicResonanceCannon6(this.transform.position, direction, this);
                    }
                    break;
                case Tower.TowerTypes.MolecularManipulator7:
                    MolecularManipulatorTower molecularManipulator = tower as MolecularManipulatorTower;
                    if (molecularManipulator != null)
                    {
                        molecularManipulator.HandleMolecularManipulator7(transform.position);
                    }
                    break;
                case Tower.TowerTypes.VortexGenerator8:
                    VortexGeneratorTower vortexGenerator = tower as VortexGeneratorTower;
                    if (vortexGenerator != null)
                    {
                        vortexGenerator.SpawnStorm(transform.position);
                    }
                    break;
                case Tower.TowerTypes.NovaGrenadeLauncher11:
                    NovaGrenadeLauncherTower novaGrenadeLauncher = tower as NovaGrenadeLauncherTower;
                    if (novaGrenadeLauncher != null)
                    {
                        novaGrenadeLauncher.HandleNovaGrenadeLauncher11(this);
                    }
                    break;
                case Tower.TowerTypes.TimeDilationBeacon12:
                    TimeDilationBeaconTower timeDilationBeacon = tower as TimeDilationBeaconTower;
                    if (timeDilationBeacon != null)
                    {
                        timeDilationBeacon.HandleTimeDilationBeacon12(this);
                    }
                    break;
                case Tower.TowerTypes.HyperspaceCannon13:
                    HyperspaceCannonTower hyperspaceCannon = tower as HyperspaceCannonTower;
                    if (hyperspaceCannon != null)
                    {
                        hyperspaceCannon.HandleHyperSpaceCannon13(this);
                    }
                    break;
                case Tower.TowerTypes.QuanntumDestabilizer14:
                    QuantumDestabilizerTower quantumDestabilizer = tower as QuantumDestabilizerTower;
                    if (quantumDestabilizer != null)
                    {
                        quantumDestabilizer.HandleQuantumDestabilizer14(this);
                    }
                    break;
                case Tower.TowerTypes.ParticleBeamCollider15:
                    ParticleBeamColliderTower particleBeamCollider = tower as ParticleBeamColliderTower;
                    if (particleBeamCollider != null)
                    {
                        particleBeamCollider.HandleParticleBeamCollider15(this);
                    }
                    break;
            }
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
