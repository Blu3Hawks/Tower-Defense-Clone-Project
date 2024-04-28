using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataFiles : MonoBehaviour, IDataPersistence
{

    public static DataFiles OnlyDataFiles;

    public BuildingInfo ScrapBuilding;
    public BuildingInfo GoldBuilding;
    public BuildingInfo ResearchBuilding;

    public int scrapBuilding_LVL = 1;
    public int goldBuilding_LVL = 1;
    public int researchBuilding_LVL = 1;

    //camera position
    public Vector3 cameraPos;

    private void Awake()
    {
        if (OnlyDataFiles != null && OnlyDataFiles == this)
        {
            Destroy(this.gameObject);
            return;
        }
        if (OnlyDataFiles == null)
        {
            OnlyDataFiles = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnEnable()
    {
        SetBuildings();
        SetCameraPos();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetBuildings();
    }

    public void SaveData(GameData data)
    {
        data.scrapBuilding_LVL = this.scrapBuilding_LVL;
        data.goldBuilding_LVL = this.goldBuilding_LVL;
        data.researchBuilding_LVL = this.researchBuilding_LVL;

    }

    public void LoadData(GameData data)
    {
        this.scrapBuilding_LVL = data.scrapBuilding_LVL;
        this.goldBuilding_LVL = data.goldBuilding_LVL;
        this.researchBuilding_LVL = data.researchBuilding_LVL;

    }

    private void SetBuildings()
    {
        //setting the buildings with a reference
        ScrapBuilding = FindObjectOfType<BuildingInfo>(ScrapBuilding);
        GoldBuilding = FindObjectOfType<BuildingInfo>(GoldBuilding);
        ResearchBuilding = FindObjectOfType<BuildingInfo>(ResearchBuilding);

        //setting their levels
        ScrapBuilding.Building_LVL = scrapBuilding_LVL;
        GoldBuilding.Building_LVL = goldBuilding_LVL;
        ResearchBuilding.Building_LVL= researchBuilding_LVL;
    }

    private void SetCameraPos()
    {
        cameraPos = FindObjectOfType<CameraSystem>().transform.position;
    }
}
