using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    [SerializeField] float healAmount = 50;
    [HideInInspector] public RespawningHeal holder = null;


    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Character")) { return; }

        other.GetComponentInParent<PlayerController>().Heal(healAmount);
        if(holder != null) {
            holder.Respawn();
        }

        Destroy(gameObject);
    }
}
