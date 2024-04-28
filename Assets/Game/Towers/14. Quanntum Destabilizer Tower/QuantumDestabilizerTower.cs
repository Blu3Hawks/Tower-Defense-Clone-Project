using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumDestabilizerTower : Tower
{
    [Header("14. Quantum Destabilizer")]
    [SerializeField] public float explosionAoeRadius;
    [SerializeField] public float maxHealthPercent;

    public void HandleQuantumDestabilizer14(Enemy enemy)
    {
        enemy.ProcessHit(damageAmountPerParticle);
        if (enemy.currentHealth <= 0)
        {
            float explosionDamage = maxHealthPercent/ 100f * enemy.maxHealth; // 30 / 100 * 10 = 3 which will be the aoe damage explosion
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionAoeRadius);
            foreach (Collider collider in hitColliders)
            {
                Enemy enemyInRange = collider.GetComponent<Enemy>();
                if (enemyInRange != null && enemyInRange != this)
                {
                    enemyInRange.ProcessHit(explosionDamage);
                }
            }
        }
    }
}
