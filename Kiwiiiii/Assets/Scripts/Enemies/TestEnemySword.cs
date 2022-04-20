using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemySword : MonoBehaviour
{
    private SOPlayerStats playerStats;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<SOPlayerStats>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ("Player"))
        {
            playerStats.health--;
        }
    }
}
