using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTP : MonoBehaviour
{
    [SerializeField] GameObject targetPos;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Character"))
        {
            Debug.Log("hej");
            collision.gameObject.transform.parent.gameObject.transform.position = targetPos.transform.position;
            collision.gameObject.transform.position = targetPos.transform.position;
        }
    }
}
