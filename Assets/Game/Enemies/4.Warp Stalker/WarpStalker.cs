using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WarpStalker : Enemy
{
    private float timer = 0f;
    [SerializeField] private float teleportTimer;
    [SerializeField] private float distanceMultiplier;

    protected override void Update()
    {
        base.Update();
        TimeCounter();
    }

    private void TimeCounter()
    {
        timer += Time.deltaTime;
        if (timer > teleportTimer && teleportTimer > 0f && distanceMultiplier > 0f)
        {
            TeleportForward();
            timer = 0f;
        }
    }

    private void TeleportForward()
    {
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(navMeshAgent.destination, path); // making sure we have our path set
        float distance = teleportTimer * startingSpeed * distanceMultiplier;

        Vector3 dirToDestination = (navMeshAgent.destination - navMeshAgent.transform.position).normalized; // direction
        Vector3 distanceVector = dirToDestination * distance; // calculating with float distance
        Vector3 nextPos = navMeshAgent.destination - distanceVector;

        if (path.corners.Length > 1)
        {
            nextPos = FindPointAheadOnPath(path, distance);
            Debug.Log(distance);
        }
        navMeshAgent.Warp(nextPos);
    }

    private Vector3 FindPointAheadOnPath(NavMeshPath path, float distanceAhead)
    {
        float accumulatedDistance = 0f;
        Vector3 lastCorner = path.corners[0];

        for (int i = 1; i < path.corners.Length; i++)
        {
            Vector3 currentCorner = path.corners[i];
            float distanceBetweenCorners = Vector3.Distance(lastCorner, currentCorner);
            if (accumulatedDistance + distanceBetweenCorners > distanceAhead)
            {
                float neededDistance = distanceAhead - accumulatedDistance;
                Vector3 direction = (currentCorner - lastCorner).normalized;
                return lastCorner + direction * neededDistance;
            }
            accumulatedDistance += distanceBetweenCorners;
            lastCorner = currentCorner;
        }
        return path.corners[path.corners.Length - 1];
    }
}
