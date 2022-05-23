using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float damage;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(-transform.forward * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            var playerController = other.gameObject.transform.parent.GetComponent<PlayerController>();
            playerController.TakeDamage(damage);
            damage = 0;
            var dmg = Instantiate(playerController.hit, other.gameObject.transform);
            Destroy(dmg, 0.3f);
        }
    }
}