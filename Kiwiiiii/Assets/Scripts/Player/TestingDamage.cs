using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDamage : MonoBehaviour
{
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();    
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == ("Enemy"))
        {
            playerHealth.DoDamage();
        }
    }
}
