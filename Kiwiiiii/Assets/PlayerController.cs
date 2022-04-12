using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float jumpForce = 2;

    public LayerMask layerMask;
    
    private Rigidbody rb;
    private PlayerControls controls;
    private bool isGrounded;
    private Vector3 jump;
    private Vector3 playerFaceDirection;

    private void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    private void FixedUpdate()
    {
        Move();
        GroundCheck();
    }

    #region Inputs

    public void Move()
    {
        var movementInput = controls.Player.Movement.ReadValue<Vector2>();
        Debug.Log(movementInput);
        var movement = new Vector3
        {
            x = movementInput.x,
            z = movementInput.y
        }.normalized;

        Vector3 dir = Camera.main.transform.TransformDirection(movement);
        rb.AddForce(dir * moveSpeed, ForceMode.Force);
    }

    private void Dash()
    {
        print("You dash lol");
    }

    private void GroundCheck()
    {
        RaycastHit hit;
        
        if(isGrounded)
        { return; }
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, layerMask))
        {
            Debug.Log("You hit the ground");
            isGrounded = true;
        }
        
    }
    public void Jump()
    {
        //in progress
        if(isGrounded)
        { rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); }    
    }
    #endregion

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();
}