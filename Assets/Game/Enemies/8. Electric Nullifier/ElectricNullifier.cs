using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricNullifier : Enemy //will be nullifying nearby towers for x amount of seconds every y amount of seconds
{
    [SerializeField] private float rangeOfEffect;
    [SerializeField] private float effectTimer;
    [SerializeField] private float disablingTowersTimer;
    List<Tower> towersInRange = new List<Tower>();
    private float currentTimer;

    protected override void Start()
    {
        base.Start();
        currentTimer = effectTimer;
    }

    protected override void Update()
    {
        base.Update();
        EffectTimer();
    }

    private void EffectTimer()
    {
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }
        if (currentTimer <= 0)
        {
            IdentifyNearbyTowers();
            currentTimer = effectTimer; //resetting the time
        }
    }

    private void IdentifyNearbyTowers()
    {
        towersInRange.Clear();
        Collider[] nearbyTowers = Physics.OverlapSphere(transform.position, rangeOfEffect);
        foreach (Collider collider in nearbyTowers)
        {
            Tower tower = collider.GetComponent<Tower>();

            if (tower != null)
            {
                towersInRange.Add(tower);
            }
        }
        StartCoroutine(ShuttingDownNearbyTowers());
    }

    private IEnumerator ShuttingDownNearbyTowers() 
    {
        foreach (Tower tower in towersInRange)
        {
            if (tower != null && !tower.generatorProtected)
            {
                tower.enabled = false;
            }
        }
        yield return new WaitForSeconds(disablingTowersTimer);

        foreach (Tower tower in towersInRange)
        {
            if (tower != null)
            {
                tower.enabled = true;
            }
        }
        towersInRange.Clear();
    }

    private void OnDestroy()
    {
        foreach (Tower tower in towersInRange)
        {
            if (tower != null && tower.enabled == false)
            {
                tower.enabled = true;
            }
        }
    }
}
