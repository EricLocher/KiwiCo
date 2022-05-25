using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Slam", menuName = "Utilities/Abilities/Slam")]
public class Slam : Ability
{
    SlamEffect _effect;
    [Header("Slam Specific Settings")]
    [SerializeField] SlamEffect effect;
    [SerializeField] AnimationCurve speed, damage, radius, force, gravity;
    [SerializeField] float minDistFromGround = 10;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float chargeTime = 2;

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
        movement.removeExtraGravity = started;
    }

    public override void Activate(InputAction.CallbackContext ctx)
    {
        timePassed = 0;
        if (!started) { return; }
        started = false;

        movement.chargeVFX.Stop();
        base.Activate(ctx);
        movement.removeExtraGravity = started;
    }

    public override void StartedAbility(InputAction.CallbackContext ctx)
    {
        started = true;
        timePassed = 0;
        movement.removeExtraGravity = started;
    }

    public override void DoAbility()
    {
        sword.down = false;
        sword.rb.MoveRotation(Quaternion.Euler(90, 0, 0));
        movement.rb.AddForce(Vector3.down * _speed, ForceMode.Impulse);
        _effect.setVariables(_force, _radius, _damage, _speed);
        _effect.IsSlamming = true;
    }

    public override void CanceledAbility(InputAction.CallbackContext ctx)
    {
        movement.chargeVFX.Stop();

        if (timePassed > 0.5f) {
            Activate(ctx);
        }

        started = false;
        timePassed = 0;
        movement.removeExtraGravity = started;
    }

    public override void UpdateAbility(float dt)
    {
        base.UpdateAbility(dt);

        if (started && (currentAmount < 1 || Physics.Raycast(movement.transform.position, Vector3.down, minDistFromGround, layerMask))) {
            movement.chargeVFX.Stop();
            started = false;
            movement.removeExtraGravity = started;
            return;
        }

        if (started) {
            timePassed += dt;
            if (timePassed > 0.25f && timePassed < 0.3f) { movement.chargeVFX.Play(); }
            movement.chargeVFX.SetVector3("pos", movement.transform.position);
            movement.chargeVFX.SetFloat("timePassed", timePassed / chargeTime);

            float sampleTime = timePassed / chargeTime;

            _damage = damage.Evaluate(sampleTime);
            _force = force.Evaluate(sampleTime);
            _speed = speed.Evaluate(sampleTime);
            _radius = radius.Evaluate(sampleTime);

            movement.rb.AddForce(Vector3.up * (gravity.Evaluate(sampleTime)), ForceMode.Force);
        }
    }
}
