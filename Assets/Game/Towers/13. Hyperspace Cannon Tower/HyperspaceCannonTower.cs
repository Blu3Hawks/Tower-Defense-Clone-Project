using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperspaceCannonTower : Tower
{
    [Header("13.HyperSpace Cannon")]
    [SerializeField] private float delayDuration;
    public void HandleHyperSpaceCannon13(Enemy enemy)
    {
        StartCoroutine(FreezeEnemy(enemy));
    }

    private IEnumerator FreezeEnemy(Enemy enemy)
    {
        enemy.ProcessHit(damageAmountPerParticle);

        Vector3 lockedEnemyPos = enemy.transform.position;
        float endTime = Time.time + delayDuration;
        while (Time.time < endTime)
        {
            if(enemy != null)
            {
                enemy.transform.position = lockedEnemyPos;
            }
            yield return null;
        }
    }
}
