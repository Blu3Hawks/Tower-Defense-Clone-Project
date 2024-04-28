using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicResonanceCannonTower : Tower
{
    [Header("6. Shockwave Effects")]
    [SerializeField] float pushBackDuration;
    [SerializeField] float pushBackMagnitude;

    [SerializeField] float coneAngle = 45f;
    [SerializeField] float coneLength = 50f;

    public void HandleSonicResonanceCannon6(Vector3 position, Vector3 direction, Enemy enemy)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, coneLength);
        foreach (Collider collider in hitColliders)
        {
            Enemy enemyInRange = collider.GetComponent<Enemy>();
            if (enemyInRange != null)
            {
                Vector3 toEnemy = enemyInRange.transform.position - position; // the distance between the enemy hit and other enemies
                float distanceToEnemy = toEnemy.magnitude;

                toEnemy.Normalize();
                float angleToEnemy = Vector3.Angle(direction, toEnemy);
                if(angleToEnemy <= coneAngle /2 && distanceToEnemy <= coneLength)
                {
                    enemyInRange.ProcessHit(damageAmountPerParticle);
                    Vector3 pushbackForce = -enemy.navMeshAgent.velocity.normalized;
                    enemyInRange.StartCoroutine(PushBackShockwave(pushbackForce, enemyInRange));
                }
            }
        }
    }

    //this method will inflict negative speed for a brief moment in a scale - so we do negative speed as if
    //we are sliding a bar back and forth. the duration will be for the timer to see it looks smooth
    public IEnumerator PushBackShockwave(Vector3 force, Enemy enemy)
    {
        Rigidbody rb = enemy.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(force * this.pushBackMagnitude, ForceMode.Impulse);

        yield return new WaitForSeconds(pushBackDuration);
        rb.isKinematic = true;
    }
}
