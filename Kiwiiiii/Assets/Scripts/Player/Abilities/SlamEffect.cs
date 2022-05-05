using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamEffect : MonoBehaviour
{
    public float radius;
    float force = 10;
    public bool IsSlamming = false;
    PlayerMovement movement;

    public void OnCreate(PlayerMovement movement)
    {
        this.movement = movement;
    }

    private void Update()
    {
        if (!IsSlamming || !movement.isGrounded) { return; }

        Slam();
    }

    public void Slam()
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in collisions) {

            if(collider.CompareTag("Player") || collider.CompareTag("Sword")) { return; }

            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb == null) { continue; }

            rb.AddExplosionForce(force, transform.position, radius, 0.0f, ForceMode.Impulse);
        }

        Debug.Log("SLam!");

        IsSlamming = false;

    }


}
