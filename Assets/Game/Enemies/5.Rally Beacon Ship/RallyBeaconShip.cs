using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RallyBeaconShip : Enemy // buffs nearby enemies with extra movespeed !
{
    [SerializeField] private float buffRadius;
    [SerializeField] private int movementSpeedBuffPercentage;
    protected override void Update()
    {
        base.Update();
        BuffingAllies();
    }

    private void BuffingAllies()
    {
        float appliedMoveSpeedBuff = (100 + movementSpeedBuffPercentage) / 100;

        
    }

}
