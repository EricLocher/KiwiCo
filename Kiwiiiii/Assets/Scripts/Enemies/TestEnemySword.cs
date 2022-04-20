using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemySword : MonoBehaviour
{
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ("Player"))
        {
            playerHealth.TakeDamage();
        }
    }
}
