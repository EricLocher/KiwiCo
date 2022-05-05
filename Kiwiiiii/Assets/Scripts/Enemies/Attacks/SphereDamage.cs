using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDamage : MonoBehaviour
{
    public float force = 2;

    private Rigidbody rb;
    public PlayerController target;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        transform.LookAt(target.transform.GetChild(0).transform);

        rb.AddForce(transform.forward * force, ForceMode.Force);
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if(col.isTrigger) { return; }

            target.DealDamage(3);

            Kill();
        }
        else if(!col.gameObject.CompareTag("Enemy")) {
            Kill();
        }

    }
}
