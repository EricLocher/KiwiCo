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
    [SerializeField] GameObject damagePopup;
    GameObject damageHolder;

    private void Start()
    {
        damageHolder = new GameObject();
        damageHolder.name = "DamageHolder";
    }

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

            var randomPos = Random.Range(-2f, 2f);
            var collPos = collision.transform.position;
            var popup = Instantiate(damagePopup, new Vector3(collPos.x + randomPos, collPos.y + 2, collPos.z + randomPos), Quaternion.identity, damageHolder.transform);
            popup.GetComponent<DamagePopup>().PopupDamage((int)damage);
        }
    }
}
