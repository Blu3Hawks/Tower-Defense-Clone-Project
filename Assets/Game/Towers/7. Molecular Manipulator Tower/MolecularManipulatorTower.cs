using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolecularManipulatorTower : Tower
{
    [Header("7. Molecular Manipulator")]
    [SerializeField] float freezeDuration;
    [SerializeField] float aoeRadiuss;
    private Dictionary<Enemy, Coroutine> enemyFreezeCoroutines = new Dictionary<Enemy, Coroutine>();

    public void HandleMolecularManipulator7(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, aoeRadiuss);
        foreach (Collider collider in hitColliders)
        {
            Enemy enemyInRange = collider.GetComponent<Enemy>();
            if (enemyInRange != null)
            {
               if(enemyFreezeCoroutines.ContainsKey(enemyInRange))
                {
                    StopCoroutine(enemyFreezeCoroutines[enemyInRange]);
                    enemyFreezeCoroutines.Remove(enemyInRange);
                }
                Coroutine newFreezeCoroutine = StartCoroutine(FreezeEnemies(freezeDuration, enemyInRange));
                enemyFreezeCoroutines.Add(enemyInRange, newFreezeCoroutine);
            }
        }
    }
    private IEnumerator FreezeEnemies(float duration, Enemy enemy)
    {
        float initialSpeed = enemy.navMeshAgent.speed;
        enemy.navMeshAgent.speed = 0f;

        while (duration > 0)
        {
            if(enemy == null)
            {
                yield break;
            }
            yield return null;
            duration -= Time.deltaTime;
        }
        if(enemy != null)
        {
            enemy.navMeshAgent.speed = initialSpeed;
        }
        if(enemyFreezeCoroutines.ContainsKey(enemy) )
        {
            enemyFreezeCoroutines.Remove(enemy);
        }
    }

    private void ResetEnemyState(Enemy enemy)
    {
        if (enemy != null && enemy.navMeshAgent != null)
        {
            enemy.navMeshAgent.speed = enemy.startingSpeed;
        }
    }

    private void OnDisable()
    {
        foreach (var entry in enemyFreezeCoroutines)
        {
            var enemy = entry.Key;
            var coroutine = entry.Value;
            if (enemy != null)
            {
                StopCoroutine(coroutine);
                ResetEnemyState(enemy);
            }
        }
        enemyFreezeCoroutines.Clear();
    }
}
