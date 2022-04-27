using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDamage : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == ("Enemy"))
        {
            playerHealth.DoDamage();
        }
    }
}
