using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public SOPlayerStats stats;
    public LayerMask layerMask;
    
    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;
    private float radius;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        radius = GetComponent<SphereCollider>().radius;
        rb = GetComponent<Rigidbody>();
    }

    #region Movement

    public void Move(Vector3 movement)
    {
        if (GameObject.FindGameObjectWithTag("CinematicCamera").GetComponent<Camera>().enabled == false)
        {
            Vector3 dir = Camera.main.transform.TransformDirection(movement);
            rb.AddForce(dir * stats.moveSpeed, ForceMode.Force);
        }
    }

    public void Dash()
    {
        
        if(stats.amountOfDashes <= 0) { return; }
        rb.AddForce(rb.velocity.normalized * stats.dashSpeed, ForceMode.Impulse);
        stats.amountOfDashes--;

        if (stats.amountOfDashes >= stats.maxDashes)
        { return; }

        StartCoroutine(DashTimer());
    }

    private IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(5f);
        stats.amountOfDashes++;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGrounded || stats.amountOfJumps > 0)
            {
                isGrounded = false;
                rb.AddForce(Vector3.up * stats.jumpForce, ForceMode.Impulse);
                stats.amountOfJumps--;
            }
        }
        //TODO: Fall multiplier
    }

    public void Spin()
    {
        animator.Play("Spin");
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
            stats.amountOfJumps = stats.maxJumps;
        }

        //if(Physics.SphereCast(transform.position, radius, Vector3.down, out hit, radius + .1f , layerMask))
        //{
        //    Debug.Log("You hit the ground");
        //    isGrounded = true;
        //}
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            GroundCheck();
        }
    }
}