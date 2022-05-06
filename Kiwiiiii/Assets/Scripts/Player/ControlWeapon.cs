using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWeapon : MonoBehaviour
{
    [SerializeField, Range(0, 10)] float sensitivity;
    [SerializeField] Rigidbody rb;
    [SerializeField] float smoothTime, swingSmoothTime;
    [SerializeField] PlayerMovement movement;
    [SerializeField] SwordBehavior sword;
    [SerializeField] float maxAngular;

    [Header("Different Behaviors")]
    [SerializeField] bool behaviour1 = true;
    [SerializeField] bool behaviour2 = false;

    float deltaY = 0;
    float timeElapsed = 0;
    bool down = false;
    bool check = true;
    public bool swing = false;

    void Start()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Weapon"), LayerMask.NameToLayer("Enemy"));
        rb.maxAngularVelocity = maxAngular;
    }

    void FixedUpdate()
    {
        if (!down) {
            if (behaviour1) {
                rb.angularVelocity = new Vector3(0, deltaY, 0);
            }
            else if (behaviour2) {
                rb.angularVelocity = new Vector3(0, rb.angularVelocity.y + deltaY, 0);
                rb.angularVelocity += new Vector3(0, deltaY, 0);
            }
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
                movement.stats.amountOfJumps = 1;
                movement.stats.jumpForce = movement.stats.defaultJumpForce * 3;

            }
        }
    }

    public void MouseInput(Vector3 input)
    {
        if(!behaviour1) { return; }
        deltaY = input.x * sensitivity * 2;
    }

    public void MouseInput(float input)
    {
        if(!behaviour2) { return; }
        if (Mathf.Sign(rb.angularVelocity.y) != Mathf.Sign(input)) {
            input *= 1000;
        }

        deltaY = input * 5;
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