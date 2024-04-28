using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public long lastUpdated;
    //building levels
    public int scrapBuilding_LVL;
    public int goldBuilding_LVL;
    public int researchBuilding_LVL;

    //resources// RSC = resources
    public int RSC_Electricity;
    public int RSC_Oil;

    //camera position
    public Vector3 cameraPos;
    //camera zoom
    public float cameraZoom;

    public GameData() 
    {
        //starting building levels
        this.scrapBuilding_LVL = 1;
        this.goldBuilding_LVL = 1;
        this.researchBuilding_LVL = 1;

        //starting resources
        this.RSC_Electricity = 100;
        this.RSC_Oil = 100;
        //camera position - we'll set it manually here for starting position but can also be set to 0 if that's the position = Vector3.zero
        cameraPos = new Vector3(25f, 40f, 3f);
        //base camera zoom
        cameraZoom = 50f;
    }

    public int GetPercentageComplete()
    {
        //this will be our current wave
        int currentLevel = 1;
        //this will be the calculation for percentage complete - so the current level divided by the max level. It might be also for difficulty,
        //but won't be calculated right now
        int percentageComplete = 0;
        return percentageComplete;
    }

}
