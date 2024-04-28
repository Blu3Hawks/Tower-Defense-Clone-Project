using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDilationBeaconTower : NovaGrenadeLauncherTower
{
    // just setting the damage to 0 but has different values !
    public void HandleTimeDilationBeacon12(Enemy enemy)
    {
        Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, aoeRadius);
        foreach (Collider collider in hitColliders)
        {
            Enemy enemyInRange = collider.GetComponent<Enemy>();
            if (enemyInRange != null)
            {
                ApplyingSlow(enemyInRange);
            }
        }
    }
}
