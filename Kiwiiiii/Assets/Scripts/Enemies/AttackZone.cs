using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == ("Player"))
        {
            playerHealth.TakeDamage();
        }
    }
}
