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
    Rigidbody swordRb;

    private void Awake()
    {
        swordRb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if(!transform.GetChild(0).gameObject.activeSelf) { return; }

        if (collision.gameObject.CompareTag("Enemy"))
        {   
            impact.Play();

            var damage = (Mathf.Abs(swordRb.angularVelocity.y) + damageMinimumValue) * damageMultiplyFactor;

            damage = Mathf.Clamp(damage, damageMinimumValue, damageMaxValue);
            collision.GetComponent<Enemy>().DealDamage(damage);
        }
    }


    
}
