using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;

    private Tower tower;
    private Enemy targetEnemy;
    private Enemy currentEnemyTarget;

    private Rigidbody rb;

    private void Start()
    {
        tower = GetComponentInParent<Tower>();
        rb = GetComponent<Rigidbody>();
        TargetNearestEnemy();
        MoveToEnemy();
    }

    private void Update()
    {
        TargetNearestEnemy();
        MoveToEnemy();
    }

    private void MoveToEnemy()
    {
        if (targetEnemy != null)
        {
            Vector3 dir = currentEnemyTarget.transform.position - rb.transform.position;
            transform.Translate(dir.normalized * bulletSpeed, Space.World);
        }
    }

    private void TargetNearestEnemy()
    {
        if (targetEnemy == null)
        {
            targetEnemy = tower.FindClosestTarget();
            currentEnemyTarget = targetEnemy;
        }
        if (currentEnemyTarget == null)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Tower tower = GetComponentInParent<Tower>();
            if (tower != null)
            {
                float damageTaken = tower.damageAmountPerParticle;
                switch (tower.towerType)
                {
                    case Tower.TowerTypes.Slowing:
                        break;
                    case Tower.TowerTypes.Basic1:
                        enemy.ProcessHit(damageTaken);
                        break;
                    case Tower.TowerTypes.Cannon2:
                        CannonTower cannonTower = tower as CannonTower;
                        if (cannonTower != null)
                        {
                            cannonTower.ExplosionDamage();
                        }
                        break;
                    case Tower.TowerTypes.NaniteSwarmLauncher3:
                        NaniteSwarmLauncherTower naniteTower = tower as NaniteSwarmLauncherTower;
                        if (naniteTower != null)
                        {
                            naniteTower.HandleNaniteSwarmLauncher(enemy);
                        }
                        break;
                    case Tower.TowerTypes.FungalSporeLauncher4:
                        FungalSporeLauncher fungalTower = tower as FungalSporeLauncher;
                        if (fungalTower != null)
                        {
                            fungalTower.HandleFungalSporeLauncher(enemy);
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
                            sonicResonanceCannon.HandleSonicResonanceCannon6(enemy.transform.position, direction, enemy);
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
                            novaGrenadeLauncher.HandleNovaGrenadeLauncher11(enemy);
                        }
                        break;
                    case Tower.TowerTypes.TimeDilationBeacon12:
                        TimeDilationBeaconTower timeDilationBeacon = tower as TimeDilationBeaconTower;
                        if (timeDilationBeacon != null)
                        {
                            timeDilationBeacon.HandleTimeDilationBeacon12(enemy);
                        }
                        break;
                    case Tower.TowerTypes.HyperspaceCannon13:
                        HyperspaceCannonTower hyperspaceCannon = tower as HyperspaceCannonTower;
                        if (hyperspaceCannon != null)
                        {
                            hyperspaceCannon.HandleHyperSpaceCannon13(enemy);
                        }
                        break;
                    case Tower.TowerTypes.QuanntumDestabilizer14:
                        QuantumDestabilizerTower quantumDestabilizer = tower as QuantumDestabilizerTower;
                        if (quantumDestabilizer != null)
                        {
                            quantumDestabilizer.HandleQuantumDestabilizer14(enemy);
                        }
                        break;
                    case Tower.TowerTypes.ParticleBeamCollider15:
                        ParticleBeamColliderTower particleBeamCollider = tower as ParticleBeamColliderTower;
                        if (particleBeamCollider != null)
                        {
                            particleBeamCollider.HandleParticleBeamCollider15(enemy);
                        }
                        break;
                }
                Destroy(this.gameObject);
            }
        }
    }
}
