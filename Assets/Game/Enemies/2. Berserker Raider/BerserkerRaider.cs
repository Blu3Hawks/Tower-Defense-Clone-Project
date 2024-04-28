using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerRaider : Enemy
{
    [SerializeField] float movespeedModifierPercentage;
    float baseSpeed;

    protected override void Start()
    {
        base.Start();
        baseSpeed = startingSpeed;
    }

    protected override void Update()
    {
        base.Update();
        MovespeedModifier();
    }

    private void MovespeedModifier()
    {
        float hpRatio = maxHealth / currentHealth; // 10 / 5 = 2
        float movespeedMultiplier = 1 + (hpRatio - 1) * (movespeedModifierPercentage / 100f);
        currentSpeed = baseSpeed * movespeedMultiplier;
        navMeshAgent.speed = currentSpeed;
    }
}
