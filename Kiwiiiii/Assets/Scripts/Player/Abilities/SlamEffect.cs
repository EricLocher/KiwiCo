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
        if (!IsSlamming) { return; }

        Slam();
    }

    public void Slam()
    {
        float _radius = movement.isGrounded ? radius : radius / 2;
        float _force = movement.isGrounded ? force : force / 2;


        Collider[] collisions = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider collider in collisions) {

            if(collider.isTrigger) { continue; }

            if(collider.CompareTag("Player") || collider.CompareTag("Sword")) { continue; }

            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb == null) { continue; }

            rb.AddExplosionForce(_force, transform.position, _radius, 0.0f, ForceMode.Impulse);

            if (collider.CompareTag("Enemy") && movement.isGrounded) {
                Enemy enemy = collider.GetComponent<Enemy>();
                enemy.DealDamage(10);
            }

        }

        vfx.SetVector3("pos", transform.position);
        if (movement.isGrounded) {
            vfx.Play();
            IsSlamming = false;
        }


    }


}
