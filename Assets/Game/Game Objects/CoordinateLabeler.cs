using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways] 
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.black;
    [SerializeField] Color blockedColor = Color.red;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;

    int coordinateMultiplier = 2;

    private void Awake() 
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        waypoint = GetComponentInParent<Waypoint>();
        //DisplayCoordinates();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying)
        {
            //DisplayCoordinates();
            UpdateObjectName();
        }
        CoordinatesColors();
        LabelToggle();
    }

    void LabelToggle()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
    void CoordinatesColors()
    {
        if(waypoint.CanPlaceOn)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
        }
    }

    /*void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x / coordinateMultiplier);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z / coordinateMultiplier);

        label.text = coordinates.x + "," + coordinates.y;
    }
    */

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
