using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Slam", menuName = "Utilities/Abilities/Slam")]
public class Slam : Ability
{
    SlamEffect _effect;
    [Header("Slam Specific Settings")]
    [SerializeField] SlamEffect effect;
    [SerializeField] float speed = 30;
    [SerializeField] float force = 10;
    [SerializeField] float radius = 6;
    [SerializeField] float damage = 10;
    [SerializeField] float minDistFromGround = 10;
    [SerializeField] LayerMask layerMask;
    [SerializeField] AnimationCurve graph;

    float _damage;
    float _radius;
    float _force;
    float _speed;

    bool started = false;
    float timePassed = 0;


    protected override void InitAbility()
    {
        _effect = Instantiate(effect, movement.transform);
        _effect.OnCreate(movement);
        movement.chargeVFX.Stop();
    }

    public override void Activate(InputAction.CallbackContext ctx)
    {
        if(!started) { return; }
        movement.chargeVFX.Stop();
        base.Activate(ctx);
    }

    public override void StartedAbility(InputAction.CallbackContext ctx)
    {
        started = true;
        timePassed = 0;
    }

    public override void DoAbility()
    {
        sword.down = false;
        sword.rb.MoveRotation(Quaternion.Euler(90, 0, 0));
        movement.rb.AddForce(Vector3.down * _speed, ForceMode.Impulse);
        _effect.setVariables(_force, _radius, _damage);
        _effect.IsSlamming = true;
        timePassed = 0;
        started = false;
    }

    public override void CanceledAbility(InputAction.CallbackContext ctx)
    {
        movement.chargeVFX.Stop();
        Debug.Log(timePassed);

        if (timePassed > 0.25f) {
            Activate(ctx);
        }

        started = false;
        timePassed = 0;
    }

    public override void UpdateAbility(float dt)
    {
        base.UpdateAbility(dt);

        if(started && (currentAmount < 1 || Physics.Raycast(movement.transform.position, Vector3.down, minDistFromGround, layerMask))) {
            movement.chargeVFX.Stop();
            started = false;
            return;
        }

        if (started) {
            timePassed += dt;
            if(timePassed > 0.25f && timePassed < 0.3f) { movement.chargeVFX.Play(); }
            movement.chargeVFX.SetVector3("pos", movement.transform.position);
            movement.chargeVFX.SetFloat("timePassed", timePassed/2);

            _damage = damage * timePassed;
            _force = force * timePassed;
            _speed = speed * timePassed;
            _radius = radius * timePassed;

            movement.rb.AddForce(Vector3.up * (3 * timePassed), ForceMode.Force);
        }
    }
}
