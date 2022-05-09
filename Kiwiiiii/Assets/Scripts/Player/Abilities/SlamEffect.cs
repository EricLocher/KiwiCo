using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SlamEffect : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [HideInInspector] public bool IsSlamming = false;

    PlayerMovement movement;
    float force = 10;
    float radius;
    float damage;

    VisualEffect vfx;
    

    public void OnCreate(PlayerMovement movement, float force, float radius, float damage)
    {
        this.movement = movement;
        this.radius = radius;
        this.force = force;
        this.damage = damage;

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


        Collider[] collisions = Physics.OverlapSphere(movement.transform.position, _radius);

        foreach (Collider collider in collisions) {

            if(collider.isTrigger) { continue; }

            if(collider.CompareTag("Player") || collider.CompareTag("Sword")) { continue; }

            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb == null) { continue; }

            if (collider.CompareTag("Enemy") && movement.isGrounded) {
                Enemy enemy = collider.GetComponent<Enemy>();

                #region Calculate Damage

                float distToEnemy = Vector3.Distance(movement.transform.position, enemy.transform.position) / radius;
                float damageToDeal = damage / distToEnemy;

                #endregion
                Debug.Log("?!?!?!");
                enemy.DealDamage(damageToDeal);
            }

            rb.AddExplosionForce(_force, movement.transform.position, _radius, 0.0f, ForceMode.Impulse);

        }

        if (movement.isGrounded) {
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.down, out hit, layerMask);

            vfx.SetVector3("pos", hit.point);
            vfx.Play();
            IsSlamming = false;
        }
    }
}
