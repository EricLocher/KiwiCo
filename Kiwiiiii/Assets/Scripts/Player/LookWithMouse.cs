using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWithMouse : MonoBehaviour
{
    Vector3 delta = Vector3.zero;
    [SerializeField, Range(0, 10)] float sensitivity;
    [SerializeField] Rigidbody rb;
    [SerializeField] float smoothTime;

    bool down = false;


    private void FixedUpdate()
    {
        if (!down) {
            rb.angularVelocity = new Vector3(delta.x, delta.y, delta.z);
        }
        else {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
            rb.angularVelocity = new Vector3((transform.localEulerAngles.x - 180) * Time.fixedDeltaTime, 0, 0);
        }
    }

    public void UpdateCamera(Vector3 input)
    {
        delta.y = input.x * sensitivity;
    }

    public void PointDown()
    {
        down = !down;

   
    }

}