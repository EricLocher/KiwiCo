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

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(type == Object.JumpPad)
                direction = transform.TransformDirection(Vector3.up * force);

            if(type == Object.BoostPad)
                direction = transform.InverseTransformDirection(collision.gameObject.GetComponent<Rigidbody>().velocity * force);

            if(type == Object.DirectionalBoostPad)
                direction = direction = transform.TransformDirection(Vector3.left * force);

            collision.gameObject.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Force);
        }
    }
}
