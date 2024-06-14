using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioReconstructor : Enemy // a self healing regenerator enemy
{
    [SerializeField] private float hpRegenPerTick;
    [SerializeField] private float regenTickTimer;
    private float timer = 0f;

    protected override void Update()
    {
        base.Update();
        HealthRegeneration();
    }

    private void HealthRegeneration()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);

        if (currentHealth < maxHealth)
        {
            if (timer >= hpRegenPerTick)
            {
                timer = 0f;
                RestoreHealth();
            }
        }
    }

    private void RestoreHealth()
    {
        if (currentHealth + hpRegenPerTick >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += hpRegenPerTick;
        }
    }
}
