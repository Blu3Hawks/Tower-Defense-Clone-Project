using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BuildingInfo : MonoBehaviour
{
    public static BuildingInfo Instance { set; get; }

    [HideInInspector] public string Building_Name;
    [HideInInspector] public string Building_Info;
    [HideInInspector] public int Building_LVL = 1;

    public int oilToUpgrade = 2;
    public int electricityToUpgrade = 2;

    DataFiles OnlyDataFiles;
    BuildingUI buildingUI;

    private void Start()
    {
        buildingUI = FindObjectOfType<BuildingUI>();
        OnlyDataFiles = FindObjectOfType<DataFiles>();
        Building_Name = this.name;
        SettingBuildingInfo();
    }

    public enum BuildingTypes 
    {
        ScrapBuilding,
        GoldBuilding,
        ResearchBuilding,
    }
    public BuildingTypes buildingTypes = new BuildingTypes();

    private void OnMouseDown()
    {
        SettingBuildingInfo();
        BuildingUI.Show_Static(this);
    }

    public void SettingBuildingInfo()
    {
        switch (buildingTypes)
        {
            case BuildingTypes.ScrapBuilding:
                //creating a new one and take the information from DataFiles. On default it's going to be 1, or 0 if building wasn't built yet,
                //and if it already exists then it takes the information from it. The default info is stored on DataFiles.
                Building_Info = "This is a gold building. It's responsible for gaining more gold per enemy destroyed during levels";
                Building_LVL = OnlyDataFiles.scrapBuilding_LVL;
                break;

            case BuildingTypes.GoldBuilding:
                Building_Info = "This is a gold building. It's responsible for gaining more gold per enemy destroyed during levels";
                Building_LVL = OnlyDataFiles.goldBuilding_LVL;
                break;

            case BuildingTypes.ResearchBuilding:
                Building_Info = "Here you are able to research more buildings, upgrades and cool stuff";
                Building_LVL = OnlyDataFiles.researchBuilding_LVL;
                break;
        }
    }

    public void UpgradeBuilding(BuildingInfo selectedBuilding)
    {
        BuildingResources buildingResources = FindObjectOfType<BuildingResources>();

        if (buildingResources == null)
        {
            Debug.Log("Whoops");
            return;
        }
        if (selectedBuilding != null && buildingResources.currentOil >= oilToUpgrade && buildingResources.currentElectricity >= electricityToUpgrade)
        {
            switch (buildingTypes)
            {
                case BuildingTypes.ScrapBuilding:
                    // Code to handle ScrapBuilding type - these all basically adding up the the set up buildings made in the DataFiles,
                    // making their info updates and then making this building, which ever type it is, to get an update as well when
                    // a change is made
                    OnlyDataFiles.scrapBuilding_LVL++;

                    break;
                case BuildingTypes.GoldBuilding:
                    // Code to handle GoldBuilding type
                    OnlyDataFiles.goldBuilding_LVL++;
                    break;
                case BuildingTypes.ResearchBuilding:
                    OnlyDataFiles.researchBuilding_LVL++;
                    // Code to handle ResearchBuilding type
                    break;
                default:
                    break;
            }
            buildingResources.BuildingUpgradeWithdraw(oilToUpgrade, electricityToUpgrade);
            SettingBuildingInfo();
            Debug.Log(Building_LVL.ToString());

            DataPersistenceManager.instance.SaveGame();
        }
        else
        {
            Debug.Log("Something's not right");
            return;
        }
    }
}
