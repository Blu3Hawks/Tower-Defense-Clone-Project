using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingUI : MonoBehaviour
{

    private static BuildingUI instance;
    private static BuildingInfo selectedBuilding;
    //texts
    TextMeshProUGUI building_Name;
    TextMeshProUGUI building_Info;
    TextMeshProUGUI building_Level;
    TextMeshProUGUI building_Stats;
    private void Awake()
    {
        instance = this;
        FindBuildingText();
        DisableBuildingUI();
    }

    public static void Show_Static(BuildingInfo building)
    {
        if (instance != null)
        {
            instance.EnableBuildingUI(building);
        }
    }

    public void EnableBuildingUI(BuildingInfo building)
    {
        selectedBuilding = building;
        gameObject.SetActive(true);
        DisplayBuildingInfo();
    }
    public void DisableBuildingUI()
    {
        selectedBuilding = null;
        gameObject.SetActive(false);
    }

    public void DisplayBuildingInfo()
    {
        building_Name.text = selectedBuilding.Building_Name;
        building_Info.text = selectedBuilding.Building_Info;
        building_Level.text = selectedBuilding.Building_LVL.ToString();
    }

    private void FindBuildingText()
    {
        building_Name = GameObject.Find("BLD_Name_Text").GetComponent<TextMeshProUGUI>();
        building_Info = GameObject.Find("BLD_Info_Text").GetComponent<TextMeshProUGUI>();
        building_Level = GameObject.Find("BLD_Level_Text").GetComponent<TextMeshProUGUI>();
        building_Stats = GameObject.Find("BLD_Stats_Text").GetComponent<TextMeshProUGUI>();
    }

    public void UpgradingBuilding()
    {
        selectedBuilding.UpgradeBuilding(selectedBuilding);
        DisplayBuildingInfo();
    }
}
