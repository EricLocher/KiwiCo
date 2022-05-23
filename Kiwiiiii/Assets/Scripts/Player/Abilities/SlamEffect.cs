using UnityEngine;
using UnityEngine.VFX;

public class SlamEffect : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    public VisualEffect slamVFX;
    [HideInInspector] public bool IsSlamming = false;

    PlayerMovement movement;
    float force = 10;
    float radius;
    float damage;
    float speed;

    public void setVariables(float force, float radius, float damage, float speed)
    {
        this.force = force;
        this.radius = radius;
        this.damage = damage;
        this.speed = speed;
    }

    public void OnCreate(PlayerMovement movement)
    {
        this.movement = movement;
    }

    void Update()
    {
        if (!IsSlamming) { return; }

        Slam();
    }

    public void Slam()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Ignore Raycast"), LayerMask.NameToLayer("Enemy"), true);
        movement.rb.AddForce(Vector3.down * speed, ForceMode.Force);
        speed += speed * Time.deltaTime;
        damage += damage * Time.deltaTime;
        radius += radius * (0.5f * Time.deltaTime);

        if (movement.isGrounded)
        {
            Collider[] collisions = Physics.OverlapSphere(movement.transform.position, radius);

            foreach (Collider collider in collisions)
            {

                if (collider.isTrigger || collider.CompareTag("Character") || collider.CompareTag("Sword")) { continue; }

                Rigidbody rb = collider.GetComponent<Rigidbody>();

                if (rb == null) { continue; }
                AudioManager.instance.PlayOnce("PlayerSlamCollide");
                if (collider.CompareTag("Enemy"))
                {
                    Character enemy = collider.GetComponent<Character>();

                    #region Calculate Damage

                    float distToEnemy = 1 - Vector3.Distance(movement.transform.position, enemy.transform.position) / radius;
                    float damageToDeal = damage * distToEnemy;

                    #endregion

                    enemy.TakeDamage(damageToDeal);
                }

                rb.AddExplosionForce(force, movement.transform.position, radius, 0.0f, ForceMode.Impulse);
            }

            RaycastHit hit;
            Physics.Raycast(movement.transform.position, Vector3.down, out hit, layerMask);

            CameraShake.Shake(0.2f, 1);

            slamVFX.SetVector3("pos", hit.point);
            slamVFX.SetFloat("size", radius);
            slamVFX.Play();
            IsSlamming = false;

            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Ignore Raycast"), LayerMask.NameToLayer("Enemy"), false);
        }
    }
}
