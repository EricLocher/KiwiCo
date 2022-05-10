using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SlamEffect : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    public VisualEffect slamVFX;
    [HideInInspector] public bool IsSlamming = false;

    PlayerMovement movement;
    float force = 10;
    float radius;
    float damage;

    public void setVariables(float force, float radius, float damage)
    {
        this.force = force;
        this.radius = radius;
        this.damage = damage;
    }

    public void OnCreate(PlayerMovement movement)
    {
        this.movement = movement;
    }

    private void Update()
    {
        if (!IsSlamming) { return; }

        Slam();
    }

    public void Slam()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Ignore Raycast"), LayerMask.NameToLayer("Enemy"), true);

        if (movement.isGrounded) {
            Collider[] collisions = Physics.OverlapSphere(movement.transform.position, radius);

            foreach (Collider collider in collisions) {

                if (collider.isTrigger) { continue; }

                if (collider.CompareTag("Player") || collider.CompareTag("Sword")) { continue; }

                Rigidbody rb = collider.GetComponent<Rigidbody>();

                if (rb == null) { continue; }

                if (collider.CompareTag("Enemy")) {
                    Enemy enemy = collider.GetComponent<Enemy>();

                    #region Calculate Damage

                    float distToEnemy = Vector3.Distance(movement.transform.position, enemy.transform.position) / radius;
                    float damageToDeal = damage / distToEnemy;

                    #endregion

                    enemy.DealDamage(damageToDeal);
                }

                rb.AddExplosionForce(force, movement.transform.position, radius, 0.0f, ForceMode.Impulse);

            }

            RaycastHit hit;
            Physics.Raycast(movement.transform.position, Vector3.down, out hit, layerMask);

            slamVFX.SetVector3("pos", hit.point);
            slamVFX.Play();
            IsSlamming = false;

            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Ignore Raycast"), LayerMask.NameToLayer("Enemy"), false);
        }
    }
}
