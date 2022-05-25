using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] Transform tpZone;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Character"))
        {
            tpZone.position = transform.position;
        }
    }
}
