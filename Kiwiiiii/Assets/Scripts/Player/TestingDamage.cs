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

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == ("Enemy"))
        {
            playerHealth.DoDamage();
        }
    }
}
