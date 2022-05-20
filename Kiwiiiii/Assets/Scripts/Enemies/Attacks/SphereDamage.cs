using UnityEngine;

public class SphereDamage : MonoBehaviour
{
    [SerializeField, Range(0, 50)] float force = 5;
    GameObject target;
    Rigidbody rb;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.LookAt(target.transform.GetChild(0).gameObject.transform);

        rb.AddForce(transform.forward * force, ForceMode.Force);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.isTrigger) { return; }
        if (collision.gameObject.CompareTag("Character"))
        {
            target.GetComponent<PlayerController>().TakeDamage(5f);
            Destroy(gameObject);
        }
    }
}
