using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDamage : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var damage = GetComponent<Rigidbody>().velocity.x * 5f;
            Debug.Log(damage);
            playerHealth.DoDamage(damage);
        }
    }
}
