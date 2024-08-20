using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarnageBattleship : Enemy //an enemy that upon death will buff nearby enemies
{
    [SerializeField] float percentageBuff; //buffs in percentages
    [SerializeField] float rangeOfEffect; // the range

    protected override void Update()
    {
        base.Update();
    }

    protected override void Start()
    {
        base.Start();
    }

    private void OnDestroy()
    {
        FindNearbyEnemies();
    }

    private void FindNearbyEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, rangeOfEffect);
        foreach (Collider collider in colliders)
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                BuffEnemy(enemy);
                Debug.Log("We just buffed an enemy !");
            }
        }
    }

    private void BuffEnemy(Enemy enemy)
    {
        float buffPercent = (percentageBuff + 100) / 100; //so buff will be 20(%) + 100 (which would be 1) and then divide by 100
        enemy.currentHealth *= buffPercent;
        enemy.startingSpeed *= buffPercent;
        enemy.navMeshAgent.speed *= buffPercent;
    }
}
