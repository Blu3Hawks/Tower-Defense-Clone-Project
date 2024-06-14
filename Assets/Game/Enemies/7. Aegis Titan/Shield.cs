using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private AegisTitan aegisTitan;

    private SphereCollider sphereCollider;
    [SerializeField] private int shieldAmount;
    private void Start()
    {
        SettingUp();
    }

    private void SettingUp()
    {
        sphereCollider = GetComponent<SphereCollider>();
        aegisTitan = GetComponentInParent<AegisTitan>();
        sphereCollider.radius = aegisTitan.rangeOfShield / 5f;
        shieldAmount = aegisTitan.startingShieldAmount;
    }

}
