using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingResources : MonoBehaviour, IDataPersistence
{
    int startingElctricity = 100;
    int startingOil = 100;

    [SerializeField] TextMeshProUGUI OilText;
    [SerializeField] TextMeshProUGUI ElectricityText;

    public int currentOil;
    public int currentElectricity;

    public int CurrentOil { get { return currentOil; } }
    public int CurrentElectricity { get { return currentElectricity; } }
    private void Awake()
    {
        currentElectricity = startingElctricity;
        currentOil = startingOil;
    }

    private void Start()
    {
        UpdateResources();
    }
    public void BuildingUpgradeWithdraw(int oilAmount, int electricityAmount)
    {
        currentElectricity -= Mathf.Abs(electricityAmount);
        currentOil -= Mathf.Abs(oilAmount);
        UpdateResources();
    }

    void UpdateResources()
    {
        OilText.text = "Oil: " + currentOil;
        ElectricityText.text = "Electricity: " + currentElectricity;
    }

    public void LoadData(GameData data)
    {
        this.currentElectricity = data.RSC_Electricity;
        this.currentOil = data.RSC_Oil;
    }
    public void SaveData(GameData data)
    {
        data.RSC_Electricity = this.currentElectricity;
        data.RSC_Oil = this.currentOil;
    }
}
