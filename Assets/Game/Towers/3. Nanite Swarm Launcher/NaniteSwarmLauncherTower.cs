using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaniteSwarmLauncherTower : Tower
{
    [SerializeField] public float damageOverTimeDuration;
    [SerializeField] public float tickDuration;
    public void HandleNaniteSwarmLauncher(Enemy enemy)
    {
        float damagePerTick = damageAmountPerParticle / (damageOverTimeDuration / tickDuration); //base damage divided by (dot divided by duration)
        StartCoroutine(DamageOverTime(enemy, damagePerTick));
    }

    public IEnumerator DamageOverTime(Enemy enemy, float damagePerTick)
    {
        float amountOfTicks = damageOverTimeDuration / tickDuration;
        if (amountOfTicks <= 0)
        {
            yield break;
        }
        for (int i = 0; i < amountOfTicks; i++)
        {
            if (enemy != null)
            {
                enemy.ProcessHit(damagePerTick);
                yield return new WaitForSeconds(tickDuration);
            }
        }
    }
}
