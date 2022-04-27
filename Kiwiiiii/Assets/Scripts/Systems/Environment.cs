using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    enum Object {
        JumpPad, 
        BoostPad 
    };
    [SerializeField, Range(0f, 1000f)]
    float force = 500f;

    Vector3 direction;

    [SerializeField]
    Object type;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(type == Object.JumpPad)
                direction = transform.TransformDirection(Vector3.up * force);

            if(type == Object.BoostPad)
                direction = transform.InverseTransformDirection(collision.gameObject.GetComponent<Rigidbody>().velocity * force);

            collision.gameObject.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Force);
        }
    }
}
