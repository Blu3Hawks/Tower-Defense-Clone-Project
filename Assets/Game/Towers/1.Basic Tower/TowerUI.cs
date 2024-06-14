using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TowerUI : MonoBehaviour
{
    private static TowerUI instance;
    private static Tower selectedTower;

    private void Awake()
    {
        instance = this;
        DisableTowerUI();
    }
    public static void Show_Static(Tower tower)
    {
        if (instance != null)
        {
            instance.EnableTowerUI(tower);
        }
    }

    public void EnableTowerUI(Tower tower)
    {
        selectedTower = tower;
        gameObject.SetActive(true);
        Debug.Log(tower.transform.position);
        transform.position = tower.transform.position;
    }

    public void DisableTowerUI()
    {
        selectedTower = null;
        gameObject.SetActive(false);
    }

    public void UpgradeButtonClicked()
    {
        if (selectedTower != null)
        {
            selectedTower.UpgradeTower(selectedTower.towerToUpgrade, selectedTower.transform.position, selectedTower.GetComponentInParent<Waypoint>());
            DisableTowerUI();
        }
    }

    public void SellButtonClicked()
    {
        if (selectedTower != null)
        {
            selectedTower.SellTower();
            DisableTowerUI();
        }
    }
}
