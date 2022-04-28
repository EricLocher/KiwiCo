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
    enum DirectionalBoost
    {
        Forward,
        Back,
        Left,
        Right,
        Up,
        Down
    };
    [SerializeField]
    DirectionalBoost DirectionBoost;

    [SerializeField, Range(0f, 1000f)]
    float force = 500f;

    [SerializeField]
    ForceMode forceMode;

    Vector3 direction;
    Vector3 DirectionBoostPad;

    void OnTriggerEnter(Collider other)
    {
        if(other.isTrigger) { return; }

        if(other.gameObject.CompareTag("Player"))
        {
            if(type == Object.JumpPad)
                direction = transform.TransformDirection(Vector3.up * force);

            if(type == Object.BoostPad)
                direction = transform.InverseTransformDirection(other.gameObject.GetComponent<Rigidbody>().velocity * force);

            if(type == Object.DirectionalBoostPad)
            {
                if (DirectionBoost == DirectionalBoost.Forward)
                    DirectionBoostPad = Vector3.forward;

                if (DirectionBoost == DirectionalBoost.Back)
                    DirectionBoostPad = Vector3.back;

                if (DirectionBoost == DirectionalBoost.Left)
                    DirectionBoostPad = Vector3.left;

                if (DirectionBoost == DirectionalBoost.Right)
                    DirectionBoostPad = Vector3.right;

                if (DirectionBoost == DirectionalBoost.Up)
                    DirectionBoostPad = Vector3.up;

                if (DirectionBoost == DirectionalBoost.Down)
                    DirectionBoostPad = Vector3.down;

                direction = transform.TransformDirection(DirectionBoostPad * force);
            }

            other.gameObject.GetComponent<Rigidbody>().AddForce(direction, forceMode);
        }
    }
}
