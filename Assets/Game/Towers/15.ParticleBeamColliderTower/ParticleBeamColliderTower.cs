using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBeamColliderTower : Tower
{
    [Header("15. Particle Beam Collider")]
    [SerializeField] public float chainRange;
    [SerializeField] public float chainCount;
    [SerializeField] public float increasedChainDamage;

    public void HandleParticleBeamCollider15(Enemy enemy)
    {
        List<Enemy> enemies = new List<Enemy>();
        Enemy currentEnemy = enemy;
        float currentDamage = damageAmountPerParticle;

        for (int i = 1; i < chainCount; i++) // The I stars at 1 because the first enemy is this enemy
        {
            if (currentEnemy != null && !enemies.Contains(currentEnemy))
            {
                // Process hit on the current enemy
                currentEnemy.ProcessHit(currentDamage);
                enemies.Add(currentEnemy);

                // Find the nearest enemy within range that is not already in the chain
                Enemy nextEnemy = FindNearestEnemyNotInChain(currentEnemy.transform.position, chainRange, enemies);

                // Update currentEnemy and currentDamage for next iteration
                currentEnemy = nextEnemy;
                currentDamage *= increasedChainDamage;
            }
            else
            {
                // No valid enemy found, or chain count reached, break the loop
                break;
            }
        }
    }

    private Enemy FindNearestEnemyNotInChain(Vector3 position, float range, List<Enemy> chain)
    {
        Collider[] colliders = Physics.OverlapSphere(position, range);
        Enemy nearestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null && !chain.Contains(enemy))
            {
                float distance = Vector3.Distance(position, enemy.transform.position);
                if (distance < minDistance)
                {
                    nearestEnemy = enemy;
                    minDistance = distance;
                }
            }
        }
        return nearestEnemy;
    }
}
