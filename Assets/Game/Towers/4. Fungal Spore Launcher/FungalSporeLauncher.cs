using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FungalSporeLauncher : Tower
{
    [Header("Damage Over Time")]
    [SerializeField] public float damageOverTimeDuration;
    [SerializeField] public float tickDuration;
    [Header("Slow Effects")]
    [SerializeField] public float slowEffect;
    [SerializeField] public float effectDuration;
    public void HandleFungalSporeLauncher(Enemy enemy)
    {
        //slow - single target
        ApplyingSlow(enemy);

        //damage over time
        float damagePerTick = damageAmountPerParticle / (damageOverTimeDuration / tickDuration);
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
    private void ApplyingSlow(Enemy enemy)
    {
        if (enemy.towersWithSlowEffect.Find(selectedTower => selectedTower.GetInstanceID() == GetInstanceID()) == null)
        {
            if (enemy.totalCurrentSlowPercent < enemy.maxSlowSpeedPercent && enemy.currentSlowEffectsAmount < enemy.maxSlowEffectsAmount)
            {
                enemy.currentSlowEffectsAmount++;
                enemy.totalCurrentSlowPercent += slowEffect;
            }
            enemy.towersWithSlowEffect.Add(this);
            StartCoroutine(ResettingSlow(enemy));
        }
        else
        {
            StartCoroutine(ResettingSlow(enemy));
        }
        //calculating the slow - gives us the correct value for totalCurrentSlowPercent
        if (enemy.totalCurrentSlowPercent >= enemy.maxSlowSpeedPercent)
        {
            enemy.totalCurrentSlowPercent = enemy.maxSlowSpeedPercent;
        }
        enemy.navMeshAgent.speed = enemy.startingSpeed * (1f - enemy.totalCurrentSlowPercent);
        StartCoroutine(ResettingSlow(enemy));
    }

    private IEnumerator ResettingSlow(Enemy enemy)
    {
        enemy.slowTimerCounter++;
        yield return new WaitForSeconds(effectDuration);
        enemy.slowTimerCounter--;

        if (enemy.slowTimerCounter == 0)
        {
            enemy.navMeshAgent.speed = enemy.startingSpeed;
            enemy.towersWithSlowEffect.Remove(this);
        }
    }
}
