using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THERE IS A BIG ISSUE HERE NEED TO FIGURE OUT HOW TO DESTROY ONLY A SPECIFIC PARTICLE AND NOT THE WHOLE PARTICLE SYSTEM

public class AegisTitan : Enemy // this enemy will have a shield that will have a area of shield. This area will be a sphere that will
                                //block any incoming damage. It will look for particle systems, or damage types or any projectiles, and will see if its parent
                                //is a tower. If it is then the particle system will be destroyed and the shield amount will get lower. When it will get to 0
                                //there will be a timer for the shield until it will get back up again fully to operation. 
                                //example given - will block 3 projectiles, when it's off it will wait 10 seconds and then have another shield, blocking 3 more projectiles
{
    public int startingShieldAmount;
    public float rangeOfShield;
    private int currentShieldAmount;

    private GameObject sphere;

    protected override void Start()
    {
        base.Start();
        currentShieldAmount = startingShieldAmount;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangeOfShield);
    }
}
