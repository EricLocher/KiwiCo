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
    [SerializeField] LayerMask layerMask;
    [SerializeField] PlayerMovement movement;

    float timeElapsed = 0;
    bool down = false;
    bool check = true;

    void Start()
    {
        rb.maxAngularVelocity = 100;
    }

    void FixedUpdate()
    {
        if (!down) {
            rb.angularVelocity = new Vector3(delta.x, delta.y, delta.z);
        }
        else if(timeElapsed < smoothTime) {
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, Quaternion.Euler(180, 0, 0), timeElapsed / smoothTime));
            timeElapsed += Time.fixedDeltaTime;
            Debug.DrawRay(transform.position, transform.forward * 10, Color.red, 10f);
        }
        else {
            rb.MoveRotation(Quaternion.Euler(180, 0, 0));
            rb.angularVelocity = Vector3.zero;
            //GroundCheck();
        }
    }

    public void UpdateCamera(Vector3 input)
    {
        delta.y = input.x * sensitivity;
    }

    public void PointDown()
    {
        if(!check) { return; }
        down = !down;
        timeElapsed = 0;
    }

    public void GroundCheck()
    {
        //if(check) { return false; }
        Debug.Log("bruh");
        Debug.DrawRay(transform.position, transform.forward * 10, Color.red, 100f);
        if(Physics.Raycast(transform.position, transform.forward, 1f, layerMask)) {
            movement.isGrounded = true;
            movement.stats.amountOfJumps = movement.stats.maxJumps;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor")) {
            check = false;
            down = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor")) {
            check = true;
        }
    }
}