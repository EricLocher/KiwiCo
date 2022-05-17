using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SwordDamage : MonoBehaviour
{
    [SerializeField] float damageMultiplyFactor = 5f;
    [SerializeField] float damageMinimumValue = 1f;
    [SerializeField] float damageMaxValue = 1f;
    [SerializeField] VisualEffect impact;
    Rigidbody swordRb;

    public List<Character> enemies = new List<Character>();

    private void Awake()
    {
        swordRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        for (int i = 0; i < enemies.Count; i++) {
            if(enemies[i] == null) { enemies.RemoveAt(i); i--; }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if(!transform.GetChild(0).gameObject.activeSelf) { return; }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Character enemy = collision.GetComponent<Character>();
            if (enemies.Contains(enemy)) { return; }

            impact.Play();

            var damage = (Mathf.Abs(swordRb.angularVelocity.y) + damageMinimumValue) * damageMultiplyFactor;

            damage = Mathf.Clamp(damage, damageMinimumValue, damageMaxValue);
            enemy.TakeDamage(damage);
            enemies.Add(enemy);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            Character enemy = collision.GetComponent<Character>();

            if (!enemies.Contains(enemy)) { return; }

            enemies.Remove(enemy);
        }
    }
}
