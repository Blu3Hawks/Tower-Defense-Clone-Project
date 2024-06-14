using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmShip : Enemy // upon death will spawn smaller enemies
{
    [Header("Duplicator stuff")]
    // on destroy instantiate X enemies on the position of death
    [SerializeField] private int amountOfEnemiesSpawned;
    [SerializeField] private Enemy enemyToSpawn;
    private Vector3 spawnPos;

    private void SpawnOnDeath()
    {
        spawnPos = transform.position;
        for (int i = 0; i < amountOfEnemiesSpawned; i++)
        {
            Instantiate(enemyToSpawn, spawnPos, Quaternion.identity);
        }
    }
    public override void ProcessHit(float damageTaken)
    {
        base.ProcessHit(damageTaken);
        if (currentHealth <= 0)
        {
            SpawnOnDeath();
        }
    }
}
