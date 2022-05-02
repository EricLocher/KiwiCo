using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BoostObject : MonoBehaviour
{
    [SerializeField]
    ForceMode forceMode;

    [SerializeField, Range(0f, 1000f)]
    protected float force = 10f;

    [SerializeField] Animator animator;
    [SerializeField] Vector3 direction = Vector3.zero;

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.isTrigger) { return; }

        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<Rigidbody>().AddForce(direction * force, forceMode);
            if(animator != null) {
                animator.Play("Boost");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + direction * force);
    }
}
