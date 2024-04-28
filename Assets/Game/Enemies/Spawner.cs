using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SingleSpawn
{
    public GameObject enemyPrefab;
    public int amountOfEnemies;
    public float spawnTimer;
    [Header("")]
    public float timeBetweenSpawns;
}

[System.Serializable]
public class WaveConfiguration
{
    public List<SingleSpawn> waves = new List<SingleSpawn>();
    public float timeBetweenWaves;
}

public class Spawner : MonoBehaviour
{
    public List<WaveConfiguration> configurations;
    private int currentWave = 0;

    private void Start()
    {
        StartCoroutine(SpawnLevel());
    }

    private IEnumerator SpawnLevel()
    {
        while(currentWave < configurations.Count)
        {
            yield return StartCoroutine(SpawnWave(configurations[currentWave]));
            yield return new WaitForSeconds(configurations[currentWave].timeBetweenWaves);
            currentWave++;
        }
    }

    private IEnumerator SpawnWave(WaveConfiguration waveConfig)
    {
        foreach(SingleSpawn singleSpawn in waveConfig.waves)
        {
            yield return StartCoroutine(SingleSpawn(singleSpawn));
            yield return new WaitForSeconds(singleSpawn.timeBetweenSpawns);
        }
    }

    private IEnumerator SingleSpawn(SingleSpawn singleSpawn)
    {
        for(int i = 0; i < singleSpawn.amountOfEnemies;  i++)
        {
            Instantiate(singleSpawn.enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(singleSpawn.spawnTimer);
        }
    }
}
