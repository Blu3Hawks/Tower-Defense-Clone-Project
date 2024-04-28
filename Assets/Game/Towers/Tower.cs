using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    //cost
    [Header("Costs")]
    [SerializeField] int goldCost = 75;
    [SerializeField] int gemCost = 3;
    //upgrading cost
    [SerializeField] int goldUpgradeCost;
    [SerializeField] int gemUpgradeCost;
    //target locator and aim
    [Header("Aiming and range")]
    [SerializeField] protected Transform towerWeapon;
    [SerializeField] public float range;
    private ParticleSystem[] attackParticles;
    protected Enemy closestTarget = null;
    //firing
    [Header("Firing")]
    public float attackRate;
    public float damageAmountPerParticle = 3f;
    //upgrading our tower
    [SerializeField] public Tower towerToUpgrade;
    //for selling
    [SerializeField] float sellPercentage = 0.7f;
    //tower types
    [Header("Tower Types Variables")]

    [Header("19. check")]
    public float hyperElectricBuffed = 0f;

    [Header("20. check")]
    public float plasmaSurgeResonator = 0f;

    public enum TowerTypes
    {
        Slowing,
        Basic1,
        Cannon2,
        NaniteSwarmLauncher3,
        FungalSporeLauncher4,
        QuantumEntaglement5,
        SonicResonanceCannon6,
        MolecularManipulator7,
        VortexGenerator8,
        QuantumEncryption9,
        NeutronStarCannon10,
        NovaGrenadeLauncher11,
        TimeDilationBeacon12,
        HyperspaceCannon13,
        QuanntumDestabilizer14,
        ParticleBeamCollider15,
        BlackHoleEmitter16,
        IonStormProjector17,
        FusionFurnaceTower18,
        HyperElectricTower19,
        PlasmaSurgeResonator20,
        QuantumWaveformEmitter21,
        NebulaDustDetector22,
    }
    public TowerTypes towerType = new TowerTypes();

    void Awake()
    {
        attackParticles = GetComponentsInChildren<ParticleSystem>();
    }
    protected virtual void Update()
    {
        FindClosestTarget();
        AimTower();
    }
    //finding closest target
    public virtual Enemy FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        float maxDis = Mathf.Infinity;
        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(towerWeapon.transform.position, enemy.transform.position);
            if (targetDistance < maxDis)
            {
                closestTarget = enemy;
                maxDis = targetDistance;
            }
        }
        return closestTarget;
    }
    //aiming tower's weapons
    public virtual void AimTower()
    {
        Attack(false);

        if (closestTarget != null)
        {
            towerWeapon.LookAt(closestTarget.transform);
            float targetDistance = Vector3.Distance(transform.position, closestTarget.transform.position);
            if (closestTarget != null && targetDistance < range)
            {
                Attack(true);
                foreach (ParticleSystem ps in attackParticles)
                {
                    ps.transform.LookAt(closestTarget.transform.position);
                }
            }
            else
            {
                Attack(false);
            }
        }
    }

    //check how many enemies are in the radius of the tower's range
    public virtual List<Enemy> GetAllEnemiesInRange()
    {
        List<Enemy> enemiesInAoe = new List<Enemy>();

        Collider[] colliders = Physics.OverlapSphere(towerWeapon.transform.position, range);
        foreach (Collider collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemiesInAoe.Add(enemy);
            }
        }
        return enemiesInAoe;
    }
    //attack method
    public virtual void Attack(bool isActive)
    {
        foreach (ParticleSystem ps in attackParticles)
        {
            var emissionModule = ps.emission;
            //setting activate
            emissionModule.enabled = isActive;
            //setting attack rate
            emissionModule.rateOverTime = attackRate;
        }
    }

    //creating tower and buying
    public virtual bool CreateTower(Tower tower, Vector3 position, Waypoint waypoint)
    {
        Resources resources = FindObjectOfType<Resources>();

        if (resources == null)
        {
            return false;
        }

        if (resources.CurrentGold >= goldCost && resources.CurrentGems >= gemCost)
        {
            var newTower = Instantiate<Tower>(tower, position, Quaternion.identity);
            newTower.transform.parent = waypoint.transform;
            resources.Withdraw(goldCost, gemCost);
            return true;
        }
        return false;
    }
    private void OnMouseDown()
    {
        TowerUI.Show_Static(this);
    }
    //upgrading the tower to selected upgrade
    public virtual void UpgradeTower(Tower towerToUpgrade, Vector3 position, Waypoint waypoint)
    {
        Resources resources = FindObjectOfType<Resources>();
        if (resources == null)
        {
            Debug.Log("Not enough resources");
            return;
        }

        if (resources.CurrentGold >= goldUpgradeCost && resources.CurrentGems >= gemUpgradeCost && towerToUpgrade != null)
        {
            Tower upgradedTower = Instantiate(towerToUpgrade, position, Quaternion.identity);
            upgradedTower.transform.parent = waypoint.transform;
            resources.Withdraw(goldUpgradeCost, gemUpgradeCost);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Something went wrong");
            return;
        }
    }

    public virtual void SellTower()
    {
        Resources resources = FindObjectOfType<Resources>();
        resources.Deposit(Mathf.RoundToInt(goldCost * sellPercentage), Mathf.RoundToInt(gemCost * sellPercentage));
        GetComponentInParent<Waypoint>().canPlaceOn = true;
        Debug.Log("Sold tower");
        Destroy(gameObject);
    }

    //for visualizing the range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(towerWeapon.transform.position, range);
    }
}