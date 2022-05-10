using UnityEngine;

public class SphereDamage : MonoBehaviour
{
    public float force = 2;

    private Rigidbody rb;
    public PlayerController target;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.LookAt(target.transform.GetChild(0).transform);

        rb.AddForce(transform.forward * force, ForceMode.Force);
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.isTrigger) { return; }
        if (col.gameObject.CompareTag("Player")) {

            target.DealDamage(3);

            Kill();
        }
        else if (!col.gameObject.CompareTag("Enemy")) {
            Debug.Log(col.gameObject.name);
            Kill();
        }

    }
}
