using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDamage : MonoBehaviour
{
    private PlayerHealth playerHealth;
    
    private Vector3 initalPos;
    
    private float maxDistance;
    private float distance = 40;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        initalPos = transform.position;
    }

    private void Update()
    {
        maxDistance = Vector3.Distance(initalPos, transform.position) + distance;

        if(distance < maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(3);
        }
     
        Destroy(gameObject);
    }
}
