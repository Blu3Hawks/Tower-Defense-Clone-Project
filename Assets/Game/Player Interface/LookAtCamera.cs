using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public GameObject mainCam;
    Quaternion changeValues = Quaternion.Euler(0f, 180f,0f);
    // Update is called once per frame
    void Update()
    {   
        transform.LookAt(mainCam.transform);
        transform.rotation *= changeValues; 
    }
}
