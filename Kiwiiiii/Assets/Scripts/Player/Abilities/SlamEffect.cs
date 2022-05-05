using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SlamEffect : MonoBehaviour
{
    public float radius;
    float force = 10;
    public bool IsSlamming = false;
    PlayerMovement movement;
    VisualEffect vfx;

    public void OnCreate(PlayerMovement movement, float force, float radius)
    {
        this.movement = movement;
        this.radius = radius;
        this.force = force;
        vfx = GetComponent<VisualEffect>();
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

            if(collider.CompareTag("Player") || collider.CompareTag("Sword")) { continue; }

            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb == null) { continue; }

            rb.AddExplosionForce(force, transform.position, radius, 0.0f, ForceMode.Impulse);
        }

        vfx.SetVector3("pos", transform.position);
        vfx.Play();
        IsSlamming = false;

    }


}
