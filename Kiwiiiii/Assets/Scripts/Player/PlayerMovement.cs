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
    [SerializeField, Range(0, 90)] float maxAngle = 45f;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public SOPlayerStats stats;
    [HideInInspector] public bool isGrounded;
    private Animator animator;
    public VisualEffect chargeVFX;

    Vector3[] groundCheckDirections = new Vector3[5] { Vector3.down, new Vector3(.5f, -.5f, 0), new Vector3(-.5f, -.5f, 0), new Vector3(0, -.5f, .5f), new Vector3(0, -.5f, -.5f) };
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

        GroundCheck();

        if (jump) {
            if (isGrounded || stats.amountOfJumps > 0) {
                if (!isGrounded) { jumpVFX.Play(); }
                string[] jumpSounds = new string[] { "jump1", "jump2", "jump3", "jump4", "jump6", "jump7", "jump8" };
                var chosen = AudioManager.instance.GetRandomAudio(jumpSounds);
                AudioManager.instance.PlayOnce(chosen);
                rb.AddForce(Vector3.up * stats.jumpForce, ForceMode.Impulse);
                stats.amountOfJumps--;
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
        for (int i = 0; i < groundCheckDirections.Length; i++) {
            if (Physics.Raycast(transform.position, groundCheckDirections[i], out hit, 2f, layerMask)) {
                if (Vector3.Angle(hit.normal, Vector3.up) > maxAngle) { continue; }
                float dist = Vector3.Distance(transform.position, hit.point);

                if (dist > characterHitBox.radius + .1f) { continue; }
                Debug.DrawRay(transform.position, groundCheckDirections[i] * 2f, Color.red);

                isGrounded = true;
                stats.amountOfJumps = stats.maxJumps;
                stats.jumpForce = stats.defaultJumpForce;
                return;
            }
        }

        isGrounded = false;
    }
    #endregion
}