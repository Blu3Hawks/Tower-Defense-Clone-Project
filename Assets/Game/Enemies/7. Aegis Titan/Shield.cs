using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private AegisTitan aegisTitan;

    private float shieldReloadTimer;
    private SphereCollider sphereCollider;
    public int shieldAmount;
    private void Start()
    {
        SettingUp();
    }
    private void SettingUp()
    {
        sphereCollider = GetComponentInChildren<SphereCollider>();
        aegisTitan = GetComponentInParent<AegisTitan>();
        sphereCollider.radius = aegisTitan.rangeOfShield / 5f;
        shieldAmount = aegisTitan.startingShieldAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        Tower tower = other.GetComponentInParent<Tower>();
        if (shieldAmount > 0)
        {
            if (tower != null)
            {
                shieldAmount--;
                if (shieldAmount <= 0)
                {
                    StartCoroutine(aegisTitan.ResettingShield());
                }

            }
            BulletMovement bulletMovement = other.GetComponent<BulletMovement>();
            if (other != tower && bulletMovement != null)
            {
                GameObject bullet = other.gameObject;
                Debug.Log(shieldAmount);
                Destroy(bullet);
            }
        }
        
    }

}
