using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehavior : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    BoxCollider boxCollider;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public bool GroundCheck()
    {
        BoxCollider collider = GetComponentInChildren<BoxCollider>();
        return (Physics.Raycast(transform.position + Vector3.up * collider.center.z, transform.up, collider.size.y * transform.localScale.y + .5f, layerMask));
    }


}
