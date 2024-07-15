using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepatchedMothership : Enemy
{
    [Header("Special Attributes")]
    [SerializeField] private float rangeOfEnemies = 10f;
    [SerializeField] private float percentageOfDecreasedRewards = 0.5f;

    private List<Enemy> nearbyEnemies = new List<Enemy>();
    private Dictionary<Enemy, (int originalGold, int originalGem)> originalRewards = new Dictionary<Enemy, (int originalGold, int originalGem)>(); //remember to add here all the rewards of enemies

    protected override void Start()
    {
        base.Start();
        StartCoroutine(UpdateNearbyEnemies());

    }
    private void OnDestroy()
    {
        // make sure rewards are restored if the mothership is destroyed
        RestoreAllRewards();
    }

    private IEnumerator UpdateNearbyEnemies()
    {
        while (true)
        {
            ApplyDecreasedRewards();
            RemoveFarEnemies();
            yield return new WaitForSeconds(1f); //I want it to be updated every 1 seconds on a default and not every frame
        }
    }

    private void ApplyDecreasedRewards()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, rangeOfEnemies);
        foreach (Collider collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null && !nearbyEnemies.Contains(enemy))
            {
                nearbyEnemies.Add(enemy);

                // Store the original rewards
                originalRewards[enemy] = (enemy.goldReward, enemy.gemReward);

                // Apply decreased rewards
                enemy.goldReward = Mathf.RoundToInt(enemy.goldReward * percentageOfDecreasedRewards);
                enemy.gemReward = Mathf.RoundToInt(enemy.gemReward * percentageOfDecreasedRewards);
            }
        }
    }

    private void RemoveFarEnemies()
    {
        List<Enemy> enemiesToRemove = new List<Enemy>();
        foreach (Enemy enemy in nearbyEnemies)
        {
            if (enemy != null)
            {
                float distanceFromEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceFromEnemy > rangeOfEnemies)
                {
                    enemiesToRemove.Add(enemy);
                }
            }
        }

        foreach (Enemy enemy in enemiesToRemove)
        {
            RestoreRewards(enemy);
            nearbyEnemies.Remove(enemy);
            originalRewards.Remove(enemy);
        }
    }

    private void RestoreRewards(Enemy enemy)
    {
        if (originalRewards.ContainsKey(enemy))
        {
            var rewards = originalRewards[enemy];
            enemy.goldReward = rewards.originalGold;
            enemy.gemReward = rewards.originalGem;
        }
    }

    private void RestoreAllRewards()
    {
        foreach (var enemy in originalRewards.Keys)
        {
            RestoreRewards(enemy);
        }
    }
}
