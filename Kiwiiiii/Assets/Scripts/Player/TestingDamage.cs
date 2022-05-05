using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TestingDamage : MonoBehaviour
{
    [SerializeField] float damageMultiplyFactor = 5f;
    [SerializeField] float damageMinimumValue = 1f;
    [SerializeField] float damageMaxValue = 1f;
    [SerializeField] VisualEffect impact;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody swordRb = GetComponent<Rigidbody>();
                
            impact.Play();

            var damage = (Mathf.Abs(swordRb.angularVelocity.y) + damageMinimumValue) * damageMultiplyFactor;

            Mathf.Clamp(damage, damageMinimumValue, damageMaxValue);

            collision.GetComponent<Enemy>().DealDamage(damage);

            swordRb.angularVelocity = new Vector3(swordRb.angularVelocity.x, 0, swordRb.angularVelocity.z);
        }
    }
}
