using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDamage : MonoBehaviour
{
    public float force = 5;
    public Vector3 direction;

    private Rigidbody rb;
    private PlayerHealth playerHealth;
    
    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody>();
        
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void Kill()
    {
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
