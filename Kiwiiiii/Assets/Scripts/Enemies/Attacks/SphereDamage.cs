using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDamage : MonoBehaviour
{
    public float force = 5;
    public Vector3 direction;

    private Rigidbody rb;
    private PlayerController player;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
            player.DealDamage(3);

            Kill();
        }

    }
}
