using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public LayerMask layerMask;
    public PlayerController playerController;
    [SerializeField] SphereCollider characterHitBox;
    [SerializeField] VisualEffect jumpVFX;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public SOPlayerStats stats;
    [HideInInspector] public bool isGrounded;
    private Animator animator;


    bool jump = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        stats = playerController.stats;
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
        if(Mathf.Abs(rb.velocity.y) <= 1)
            GroundCheck();
        else {
            isGrounded = false;
        }
        if (jump) {
            if (isGrounded || stats.amountOfJumps > 0) {
                rb.AddForce(Vector3.up * stats.jumpForce, ForceMode.Impulse);
                stats.amountOfJumps--;
                if(!isGrounded)
                    jumpVFX.Play();
            }
            jump = false;
        }

    }

    public void Spin()
    {
        animator.Play("Spin");
    }

    private void GroundCheck()
    {
        InputSystem.Update();

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f, layerMask)) {
            if(Vector3.Angle(hit.normal, Vector3.up) > 45f) { isGrounded = false; return; }
            isGrounded = true;
            stats.amountOfJumps = stats.maxJumps;
            stats.jumpForce = stats.defaultJumpForce;
        }
        else {
            isGrounded = false;
        }
    }
    #endregion

}