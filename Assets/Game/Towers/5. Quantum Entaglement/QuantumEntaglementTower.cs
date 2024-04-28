using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumEntaglementTower : Tower
{
    public void HandleQuantumEntaglement5()
    {
        //count the amount of enemies in the range
        List<Enemy> enemyList = GetAllEnemiesInRange();
        //amount of enemies in tower's range
        int numberOfEnemiesInAOE = enemyList.Count;
        //reducing the amount of damage - but adding 10% damage   
        float damagePerEnemy = damageAmountPerParticle / numberOfEnemiesInAOE;
        if (enemyList.Count > 1)// if there is more than 1 enemy in the list then we will just add more damage and per enemy, but dividing will keep on doing the same
        {
            damagePerEnemy += damageAmountPerParticle * 0.1f;
        }
        foreach (Enemy enemy in enemyList)
        {
            enemy.ProcessHit(damagePerEnemy);
            Debug.Log(numberOfEnemiesInAOE + "amount of enemies detected in range");
        }
    }
}
