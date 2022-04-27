using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWeapon : MonoBehaviour
{
    [SerializeField, Range(0, 10)] float sensitivity;
    [SerializeField] Rigidbody rb;
    [SerializeField] float smoothTime;
    [SerializeField] PlayerMovement movement;
    [SerializeField] SwordBehavior sword;
    [SerializeField] float maxAngular;

    float deltaY = 0;

    float timeElapsed = 0;
    bool down = false;
    bool check = true;

    void Start()
    {
        rb.maxAngularVelocity = maxAngular;
    }

    void FixedUpdate()
    {
        if (!down) {
            rb.angularVelocity = new Vector3(0, deltaY, 0);
        }
        else {
            if (timeElapsed < smoothTime) {
                rb.MoveRotation(Quaternion.Lerp(rb.rotation, Quaternion.Euler(180, 0, 0), timeElapsed / smoothTime));
                timeElapsed += Time.fixedDeltaTime;
            }
            else {
                rb.MoveRotation(Quaternion.Euler(180, 0, 0));
                rb.angularVelocity = Vector3.zero;
            }

            if (sword.GroundCheck()) {
                movement.isGrounded = true;
                movement.stats.amountOfJumps = movement.stats.maxJumps;
            }
            
        }
    }

    public void MouseInput(Vector3 input)
    {
        deltaY = input.x * sensitivity;
    }

    public void PointDown()
    {
        if(!check) { return; }
        down = !down;
        timeElapsed = 0;
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