using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDamage : MonoBehaviour
{
    private PlayerHealth playerHealth;


    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
     
    }



    private void Kill()
    {
        Debug.Log("kill sphere");
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(3);

            Kill();
        }

    }
}
