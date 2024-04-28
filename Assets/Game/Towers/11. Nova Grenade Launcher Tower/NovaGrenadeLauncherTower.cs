using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaGrenadeLauncherTower : Tower
{
    [Header("Slow Effects")]
    [SerializeField] public float slowEffect;
    [SerializeField] public float effectDuration;

    public float aoeRadius;
    public void HandleNovaGrenadeLauncher11(Enemy enemy)
    {
        enemy.ProcessHit(damageAmountPerParticle);

        Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, aoeRadius);
        foreach (Collider collider in hitColliders)
        {
            Enemy enemyInRange = collider.GetComponent<Enemy>();
            if (enemyInRange != null)
            {
                ApplyingSlow(enemyInRange);
            }
        }
    }

    public void ApplyingSlow(Enemy enemy)
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

        if (enemy.slowTimerCounter == 0 && enemy != null)
        {
            enemy.navMeshAgent.speed = enemy.startingSpeed;
            enemy.towersWithSlowEffect.Remove(this);
        }
    }
}
