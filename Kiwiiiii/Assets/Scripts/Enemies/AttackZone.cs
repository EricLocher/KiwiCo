using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == ("Player"))
        {
            player.TakeDamage(0.1f);
        }
    }
}
