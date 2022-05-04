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
            var swordVelocity = GetComponent<Rigidbody>().angularVelocity;
                
            impact.Play();

            var damage = (Mathf.Abs(swordVelocity.y) + damageMinimumValue) * damageMultiplyFactor;

            Debug.Log(damage);

            Mathf.Clamp(damage, damageMinimumValue, damageMaxValue);

            collision.GetComponent<Enemy>().DealDamage(damage);

            swordVelocity.y = 0;

        }
    }
}
