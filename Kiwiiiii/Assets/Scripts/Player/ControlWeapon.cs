using UnityEngine;

public class ControlWeapon : MonoBehaviour
{
    [SerializeField, Range(0, 10)] float sensitivity;
    [SerializeField] float smoothTime, swingSmoothTime;
    public PlayerMovement movement;
    [SerializeField] SwordBehavior sword;
    [SerializeField] float maxAngular;

    public Rigidbody rb;
    BoxCollider damageCollider;

    public bool down = false;
    bool sheath = false;
    bool check = true;

    float deltaY = 0;
    float timeElapsed = 0;

    void Start()
    {
        damageCollider = GetComponent<BoxCollider>();
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Weapon"), LayerMask.NameToLayer("Enemy"));
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Weapon"), LayerMask.NameToLayer("Barrier"));
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Weapon"), LayerMask.NameToLayer("IgnoreWeapon"));
        rb.maxAngularVelocity = maxAngular;
    }

    void FixedUpdate()
    {
        if (!down)
        {
            rb.angularVelocity = new Vector3(0, rb.angularVelocity.y + deltaY, 0);
            rb.angularVelocity += new Vector3(0, deltaY, 0);
        }
        else
        {
            if (timeElapsed < smoothTime)
            {
                rb.MoveRotation(Quaternion.Lerp(rb.rotation, Quaternion.Euler(180, 0, 0), timeElapsed / smoothTime));
                timeElapsed += Time.fixedDeltaTime;
            }
            else
            {
                rb.MoveRotation(Quaternion.Euler(180, 0, 0));
                rb.angularVelocity = Vector3.zero;
            }

            if (sword.GroundCheck())
            {
                movement.isGrounded = true;
                movement.stats.amountOfJumps = 1;
                movement.stats.jumpForce = movement.stats.defaultJumpForce * 3;

            }
        }
    }

    #region Inputs

    public void SheathWeapon()
    {
        AudioManager.instance.PlayOnce("SwordSheath");
        sheath = !sheath;
        down = false;

        sword.gameObject.SetActive(!sheath);
        damageCollider.enabled = !sheath;

        if (sheath)
        {
            rb.MoveRotation(Quaternion.Euler(90, 0, 0));
        }
    }

    public void MouseInput(float input)
    {
        if (Mathf.Sign(rb.angularVelocity.y) != Mathf.Sign(input))
        {
            input *= 1000;
        }

        deltaY = input * 5;
    }

    public void PointDown()
    {
        if (!check) { return; }
        down = !down;
        timeElapsed = 0;
    }

    #endregion

    #region Collision
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            check = false;
            down = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            check = true;
        }
    }
    #endregion
}