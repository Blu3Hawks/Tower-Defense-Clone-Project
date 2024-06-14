using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : Tower
{
    [Header("Radius of Explosion")]
    [SerializeField] private float radius;

    public void ExplosionDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.ProcessHit(damageAmountPerParticle);
            }
        }
    }

}
