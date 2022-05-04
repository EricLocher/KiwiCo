using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    private PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ("Player"))
        {
            //playerHealth.TakeDamage();
        }
    }
}
