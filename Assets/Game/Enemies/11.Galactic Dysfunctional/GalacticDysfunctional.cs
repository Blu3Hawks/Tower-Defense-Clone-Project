using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalacticDysfunctional : Enemy //will be randomly generated between a few types - each type will reduce a value in nearby towers' attributes
{
    private enum DebuffType
    {
        DamageReduction,
        AttackSpeedReduction,
        RangeReduction,
    }

    // Types to have - damage reduction, attack speed reduction, range reduction
    [Header("Values")]
    [SerializeField] private float rangeOfEffect;
    [SerializeField] private float debuffDuration; // for how long will the debuff last
    [SerializeField] private float debuffTimer; // in how long will a debuff take action
    [SerializeField] private float reductionPercentage;

    private float calculatedValue; // will take the value we set and take the completing value of it to 1 - so if I put 30% off it means there's 70% on

    private float currentDebuffTimer; // in how long will a debuff take action

    private List<Tower> nearbyTowers = new List<Tower>();
    private static System.Array debuffValues = System.Enum.GetValues(typeof(DebuffType));

    protected override void Start()
    {
        base.Start();
        RandomSetup();
        currentDebuffTimer = debuffTimer;
        calculatedValue = 1 - (reductionPercentage / 100f);
    }

    private DebuffType debuffType;

    private void RandomSetup()
    {
        debuffType = (DebuffType)debuffValues.GetValue(Random.Range(0, debuffValues.Length));
        Debug.Log(this.debuffType.ToString());
    }

    protected override void Update()
    {
        base.Update();
        DebuffActivationTimer();
    }

    private void DebuffActivationTimer() // in how long will the debuff be activated
    {
        if (currentDebuffTimer > 0)
        {
            currentDebuffTimer -= Time.deltaTime;
        }
        if (currentDebuffTimer <= 0)
        {
            StartCoroutine(DebuffingNearbyTowers());
            Debug.Log("We made a debuff to nearby towers!");
            currentDebuffTimer = debuffTimer;
        }
    }

    private IEnumerator DebuffingNearbyTowers()
    {
        LookForNearbyTowers();
        foreach (Tower tower in nearbyTowers)
        {
            switch (debuffType)
            {
                case DebuffType.DamageReduction:
                    DamageReductionDebuff(tower);
                    break;
                case DebuffType.AttackSpeedReduction:
                    AttackSpeedReduction(tower);
                    break;
                case DebuffType.RangeReduction:
                    AttackRangeReduction(tower);
                    break;
            }
        }

        yield return new WaitForSeconds(debuffDuration);

        ResetValues();
        nearbyTowers.Clear();
    }

    private void ResetValues()
    {
        foreach (Tower tower in nearbyTowers)
        {
            switch (debuffType)
            {
                case DebuffType.DamageReduction:
                    DamageReductionReset(tower);
                    break;
                case DebuffType.AttackSpeedReduction:
                    AttackSpeedReset(tower);
                    break;
                case DebuffType.RangeReduction:
                    AttackRangeReset(tower);
                    break;
            }
        }
        Debug.Log("We just reset the values");
    }

    private void LookForNearbyTowers()
    {
        nearbyTowers.Clear();
        Collider[] colliders = Physics.OverlapSphere(transform.position, rangeOfEffect);
        foreach (Collider collider in colliders)
        {
            Tower tower = collider.GetComponent<Tower>();
            if (tower != null)
            {
                nearbyTowers.Add(tower);
            }
        }
    }

    private void DamageReductionDebuff(Tower tower)
    {
        // Let's say the enemy does 3 damage.
        // Let's say our reduced percentage is 30 - meaning 0.7 of the original damage - resulting in 2.1 damage.
        tower.damageAmountPerParticle *= calculatedValue;
    }

    private void DamageReductionReset(Tower tower)
    {
        tower.damageAmountPerParticle /= calculatedValue;
    }

    private void AttackSpeedReduction(Tower tower)
    {
        tower.attackRate *= calculatedValue;
    }

    private void AttackSpeedReset(Tower tower)
    {
        tower.attackRate /= calculatedValue;
    }

    private void AttackRangeReduction(Tower tower)
    {
        tower.range *= calculatedValue;
    }

    private void AttackRangeReset(Tower tower)
    {
        tower.range /= calculatedValue;
    }
}
