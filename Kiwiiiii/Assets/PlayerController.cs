using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float dashSpeed = 100;
    [SerializeField] float jumpForce = 5;

    public LayerMask layerMask;
    
    private Rigidbody rb;
    private PlayerControls controls;
    private bool isGrounded;

    private float radius;

    private void Awake()
    {
        radius = GetComponent<SphereCollider>().radius;
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        if (controls.Player.Jump.triggered)
        {
            Jump();
        }
    }

    #region Inputs

    public void Move()
    {
        var movementInput = controls.Player.Movement.ReadValue<Vector2>();

        var movement = new Vector3
        {
            x = movementInput.x,
            z = movementInput.y
        }.normalized;

        Vector3 dir = Camera.main.transform.TransformDirection(movement);
        rb.AddForce(dir * moveSpeed, ForceMode.Force);
    }

    public void Dash()
    {
        rb.AddForce(rb.velocity.normalized * dashSpeed, ForceMode.Impulse);
    }

    private void GroundCheck()
    {
        if (isGrounded)
        { return; }

        InputSystem.Update();
        //if (Physics.CheckSphere(transform.position, radius + 0.1f))
        //{
        //    isGrounded = true;
        //    print("grounded");
        //}

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, radius + 1f, layerMask))
        {
            isGrounded = true;
        }

        //if(Physics.SphereCast(transform.position, radius, Vector3.down, out hit, radius + .1f , layerMask))
        //{
        //    Debug.Log("You hit the ground");
        //    isGrounded = true;
        //}
    }
    public void Jump()
    {
        //in progress
        if(isGrounded)
        { 
            isGrounded = false;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  
        }    
    }

    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            GroundCheck();
        }
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();
}