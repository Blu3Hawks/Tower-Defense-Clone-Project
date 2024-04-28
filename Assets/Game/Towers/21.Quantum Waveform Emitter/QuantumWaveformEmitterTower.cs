using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumWaveformEmitterTower : Tower
{
    //when hitting an enemy, it will then make it disappear for a moment or two and then get it back.
    //basically saying the enemies hit will be disabled for a given amount.
    //so a counter is needed and also a time looper

    [SerializeField] float disablingTimer;

    private void OnParticleCollision(GameObject other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if(enemy != null)
        {
            Debug.Log("We've hit an enemy");
        }
    }
}
