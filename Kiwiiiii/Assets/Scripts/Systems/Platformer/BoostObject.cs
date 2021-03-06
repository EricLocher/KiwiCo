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

        if (other.gameObject.CompareTag("Character")) {
            other.gameObject.GetComponent<Rigidbody>().AddForce(direction * force, forceMode);
            if(animator != null) {
                animator.Play("Boost");
                AudioManager.instance.PlayOnce("Boost");

                PlayerMovement movement = other.gameObject.GetComponent<PlayerMovement>();
                movement.jumpVFX.Play();
                movement.stats.amountOfJumps = movement.stats.maxJumps;
                movement.stats.jumpForce = movement.stats.defaultJumpForce;
            }
        }
        else if (other.gameObject.CompareTag("Sword")) {
            ControlWeapon sword = other.gameObject.GetComponent<ControlWeapon>();

            if(!sword.down) { return; }

            sword.movement.GetComponent<Rigidbody>().AddForce(direction * force, forceMode);
            if (animator != null) {
                animator.Play("Boost");
                AudioManager.instance.PlayOnce("Boost");

                PlayerMovement movement = other.gameObject.GetComponent<PlayerMovement>();
                movement.jumpVFX.Play();
                movement.stats.amountOfJumps = movement.stats.maxJumps;
                movement.stats.jumpForce = movement.stats.defaultJumpForce;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + direction * force);
    }
}
