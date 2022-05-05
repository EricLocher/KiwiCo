using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTP : MonoBehaviour
{
    [SerializeField] Vector3 targetPos;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {

            collision.gameObject.transform.parent.transform.position = (transform.position + targetPos);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + targetPos);
    }

}
