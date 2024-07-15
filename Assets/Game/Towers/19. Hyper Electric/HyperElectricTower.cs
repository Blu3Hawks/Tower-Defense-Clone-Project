using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperElectricTower : Tower
{
    [Header("Modifiers")]
    //has already a range from the Tower's inheritence
    [SerializeField] float increasedRange;
    [SerializeField] float increasedDamageModifier;

    List<Tower> towersInRange = new List<Tower>();

    protected override void Update()
    {
        base.Update();
        GetAllTowersInRange();
    }
    private List<Tower> GetAllTowersInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider collider in colliders)
        {
            Tower tower = collider.GetComponent<Tower>();
            if (tower != null)
            {
                if (!towersInRange.Contains(tower))
                {
                    towersInRange.Add(tower);
                    ApplyBuffs();
                }
            }
        }
        return towersInRange;
    }

    private void ApplyBuffs()
    {
        foreach (Tower tower in towersInRange)
        {
            EnableTowerBuffs(tower);
        }
    }
    private void EnableTowerBuffs(Tower tower) //might change it later idk about the name sounds weird to me
    {
        tower.range += increasedRange;
        tower.damageAmountPerParticle *= increasedDamageModifier;
    }

    private void DisableTowerBuffs(Tower tower)
    {
        tower.range -= increasedRange;
        tower.damageAmountPerParticle /= increasedDamageModifier;
    }

    public override void SellTower()
    {
        base.SellTower();
        OnDestroyingTower();
    }

    private void OnDestroyingTower()
    {
        // taking every tower in towersInRange and giving a value of its index - like a reference?
        foreach (Tower tower in towersInRange)
        {
            DisableTowerBuffs(tower);
        }
        towersInRange.Clear();
    }
}
