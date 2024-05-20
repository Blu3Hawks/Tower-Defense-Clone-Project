using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class RallyBeaconShip : Enemy // buffs nearby enemies with extra movespeed !
{
    [SerializeField] private float buffRadius;
    [SerializeField] private int movementSpeedBuffPercentage;

    protected override void Update()
    {
        base.Update();
        ApplyingNearbyEnemiesBuff();
    }

    private void ApplyingNearbyEnemiesBuff() // needs a change - this is buffing the movespeed every time ! needs to be only once !
    {
        float appliedMoveSpeedBuff = (100 + movementSpeedBuffPercentage) / 100;

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, buffRadius);
        foreach (Collider collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                BuffEnemy(enemy, appliedMoveSpeedBuff);
            }
        }
    }

    private void BuffEnemy(Enemy enemy, float speedBuff)
    {
        NavMeshAgent agent = enemy.navMeshAgent;
        agent.speed += speedBuff;
    }
}
