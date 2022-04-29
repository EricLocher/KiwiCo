using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDamage : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] float damageMultiplyFactor = 5f;
    [SerializeField] float damageMinimumValue = 1f;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var swordVelocity = GetComponent<Rigidbody>().velocity;
            var damage = (Mathf.Abs(swordVelocity.y) + damageMinimumValue) * damageMultiplyFactor;
            playerHealth.DoDamage(damage);
        }
    }
}
