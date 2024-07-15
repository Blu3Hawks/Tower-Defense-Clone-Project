using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalacticDysfunctional : Enemy //will be randomly generated between a few types - each type will reduce a value in nearby towers' attributes
{
    // Types to have - damage reduction, attack speed reduction, range reduction
    private enum DebuffType
    {
        DamageReduction,
        AttackSpeedReduction,
        RangeReduction
    }

    private DebuffType debuffType = new DebuffType();

    private void RandomSetup()
    {
        
    }

}
