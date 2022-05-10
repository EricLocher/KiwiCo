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

    public List<Enemy> enemies = new List<Enemy>();

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
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemies.Contains(enemy)) { return; }

            impact.Play();

            var damage = (Mathf.Abs(swordRb.angularVelocity.y) + damageMinimumValue) * damageMultiplyFactor;

            damage = Mathf.Clamp(damage, damageMinimumValue, damageMaxValue);
            enemy.DealDamage(damage);
            enemies.Add(enemy);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            Enemy enemy = collision.GetComponent<Enemy>();

            if (!enemies.Contains(enemy)) { return; }

            enemies.Remove(enemy);
        }
    }
}
