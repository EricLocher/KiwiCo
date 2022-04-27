using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    enum Object {
        JumpPad, 
        BoostPad, 
        DirectionalBoostPad
    };
    [SerializeField]
    Object type;

    [SerializeField, Range(0f, 1000f)]
    float force = 500f;

    Vector3 direction;

    [SerializeField]
    Vector3 DirectionalBoostPad = Vector3.left;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(type == Object.JumpPad)
                direction = transform.TransformDirection(Vector3.up * force);

            if(type == Object.BoostPad)
                direction = transform.InverseTransformDirection(other.gameObject.GetComponent<Rigidbody>().velocity * force);

            if(type == Object.DirectionalBoostPad)
                direction = direction = transform.TransformDirection(DirectionalBoostPad * force);

            other.gameObject.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Force);
        }
    }
}
