using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public LayerMask layerMask;

    [HideInInspector] public SOPlayerStats stats;
    [HideInInspector] public bool isGrounded;
    public Rigidbody rb;
    private Animator animator;
    private float radius;
    [SerializeField] SphereCollider characterHitBox;
    [SerializeField] VisualEffect slam;

    bool jump = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        radius = characterHitBox.radius;
        rb = GetComponent<Rigidbody>();
        stats = GetComponentInParent<PlayerController>().stats;

        stats.amountOfDashes = stats.maxDashes;
    }

    #region Movement
    public void Move(Vector3 movement)
    {
        Vector3 dir = Camera.main.transform.TransformDirection(movement);
        rb.AddForce(dir * stats.moveSpeed, ForceMode.Force);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        jump = true;
        //TODO: Fall multiplier
    }

    void FixedUpdate()
    {
        if (jump) {
            if (isGrounded || stats.amountOfJumps > 0) {
                rb.AddForce(Vector3.up * stats.jumpForce, ForceMode.Impulse);
                stats.amountOfJumps--;
            }
            jump = false;
        }

        GroundCheck();
    }

    public void Spin()
    {
        animator.Play("Spin");
    }

    private void GroundCheck()
    {

        InputSystem.Update();
        //if (Physics.CheckSphere(transform.position, radius + 0.1f))
        //{
        //    isGrounded = true;
        //    print("grounded");
        //}

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.5f, layerMask)) {
            isGrounded = true;
            stats.amountOfJumps = stats.maxJumps;
            stats.jumpForce = stats.defaultJumpForce;
        }
        else {
            isGrounded = false;
        }

        //if(Physics.SphereCast(transform.position, radius, Vector3.down, out hit, radius + .1f , layerMask))
        //{
        //    Debug.Log("You hit the ground");
        //    isGrounded = true;
        //}
    }
    #endregion

}