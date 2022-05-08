using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SlashProjectile : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] VisualEffect vfx;

    float damage;

    public void Slash(ControlWeapon sword, float speed, float damage, float lifeTime)
    {
        vfx.SetFloat("lifeTime", lifeTime);
        this.damage = damage;

        Vector3 force = transform.forward * speed;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = sword.rb.worldCenterOfMass;

        rb.AddForce(force, ForceMode.Impulse);

        Destroy(gameObject, lifeTime + 0.7f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.DealDamage(damage);
        }
    }
}
