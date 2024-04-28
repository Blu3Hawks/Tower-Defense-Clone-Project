using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private static Tower selectedTower;

    public Color hoverColor;
    private Color startingColor;
    private Renderer rend;

    [SerializeField] public bool canPlaceOn;
    void Start()
    {
        rend = GetComponent<Renderer>();
        startingColor = rend.material.color;
    }
    public bool CanPlaceOn { get { return canPlaceOn; } }

    private void OnMouseDown()
    {
        Debug.Log("Clicked");
        if (canPlaceOn && selectedTower != null)
        {
            bool isPlaced = selectedTower.CreateTower(selectedTower, transform.position, this);
            canPlaceOn = !isPlaced;
            Debug.Log("Tower Created");
        }
    }

    public void SetNewTower(Tower towerToSelect)
    {
        selectedTower = towerToSelect;
    }

    public void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }
    private void OnMouseExit()
    {
        rend.material.color = startingColor;
    }
}
