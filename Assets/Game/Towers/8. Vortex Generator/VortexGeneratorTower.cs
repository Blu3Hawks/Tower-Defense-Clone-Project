using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexGeneratorTower : Tower
{
    [Header("8. Storm")]
    [SerializeField] public GameObject stormPrefab;
    [SerializeField] public float stormDuration;
    [SerializeField] public float stormTickDuration;
    [SerializeField] public float stormTickDamage;
    [SerializeField] public float stormPullStrength;

    public void HandleVortexGenerator8(Enemy enemy)
    {
        SpawnStorm(enemy.transform.position);
    }

    public void SpawnStorm(Vector3 spawnPoint)
    {
        GameObject newStorm = Instantiate(stormPrefab, spawnPoint, Quaternion.identity);
        if (newStorm != null)
        {
            newStorm.transform.parent = transform;
            Storm storm = newStorm.GetComponent<Storm>();
            storm.Initialize(stormDuration, stormTickDamage, stormTickDuration, stormPullStrength);
        }
        else
        {
            Debug.LogError("Storm prefab does not have a StormDamage component attached.");
        }
    }
}
