using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaSurgeResonatorTower : Tower
{
    //adds damage and attack speed
    [Header("Modifiers")]
    //has already a range from the Tower's inheritence
    [SerializeField] float increasedAttackSpeedModifier;
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
                    tower.plasmaSurgeResonator++;

                }
            }
        }
        return towersInRange;
    }

    private void ApplyBuffs()
    {
        foreach (Tower tower in towersInRange)
        {
            if (tower.plasmaSurgeResonator <= 0f)
            {
                EnableTowerBuffs(tower);
            }
        }
    }
    private void EnableTowerBuffs(Tower tower) //might change it later idk about the name sounds weird to me
    {
        tower.attackRate *= increasedAttackSpeedModifier;
        tower.damageAmountPerParticle *= increasedDamageModifier;
    }

    private void DisableTowerBuffs(Tower tower)
    {
        tower.attackRate /= increasedAttackSpeedModifier;
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
        for (int i = towersInRange.Count - 1; i >= 0; i--)
        {
            Tower tower = towersInRange[i];
            tower.plasmaSurgeResonator--;

            if (tower.plasmaSurgeResonator <= 0f)
            {
                DisableTowerBuffs(tower);
                Debug.Log("Debuffed towers");
                tower.plasmaSurgeResonator = 0f;
            }
            else
            {
                Debug.Log("Not just yet");
                Debug.Log(tower.plasmaSurgeResonator);
            }
        }
        towersInRange.Clear();
    }
}
