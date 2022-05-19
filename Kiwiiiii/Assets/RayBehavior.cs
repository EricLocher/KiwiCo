using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayBehavior : MonoBehaviour
{
    [SerializeField] public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            other.gameObject.transform.parent.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}