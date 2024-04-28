using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleTower : Tower
{
    [Header("Black Hole Variables")]
    public GameObject blackHolePrefab; // Assign this in the Unity Inspector
    public float blackHoleSpeed;
    public float CDTime = 3f; // Time in seconds between shots - tracks down the current timer
    private float cooldownTimer; //default time for the cooldown

    private void Start()
    {
        cooldownTimer = CDTime;
    }

    protected override void Update()
    {
        base.Update(); //coppying the same update
        AttackTimer(); // adding to it - which is why I need to inherit and protect it. If I wanted it to act the same I wouldn't write the base.Update;
    }

    private void AttackTimer()
    {
        // Decrease the cooldown timer
        if (CDTime > 0)
        {
            Debug.Log("The timer for shooting is still up - it's charging, please wait");
            CDTime -= Time.deltaTime;
        }

        // Check if it's time to attack
        if (CDTime <= 0 && closestTarget != null)
        {
            ShootBlackHole();
            CDTime = cooldownTimer; // Reset the cooldown timer after attack
            Debug.Log("Attacking"); //some comments to see if that's working right. Or not :)
        }
    }

    private void ShootBlackHole()
    {
        GameObject blackHoleObject = Instantiate(blackHolePrefab, transform.position, Quaternion.identity);
        BlackHole blackHoleScript = blackHoleObject.GetComponent<BlackHole>();

        if (blackHoleScript != null)
        {
            Enemy closestEnemy = FindClosestTarget(); // Method to find the closest enemy
            if (closestEnemy != null)
            {
                Vector3 targetDirection = (closestEnemy.transform.position - transform.position);
                Vector3 shootDirection = new Vector3(targetDirection.x, 0f, targetDirection.z).normalized;
                blackHoleScript.InitializeBlackHole(shootDirection, blackHoleSpeed);
            }
        }
        else
        {
            Debug.LogError("BlackHole script not found on the prefab.");
        }
    }

}
