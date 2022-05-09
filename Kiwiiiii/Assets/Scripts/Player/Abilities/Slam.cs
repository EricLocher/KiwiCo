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

    float timePassed = 0;

    protected override void InitAbility()
    {
        _effect = Instantiate(effect, movement.transform);
        _effect.OnCreate(movement, force, radius, damage);
    }

    public override void Activate(InputAction.CallbackContext ctx)
    {
        RaycastHit hit;

        if(Physics.Raycast(movement.transform.position, Vector3.down, out hit, layerMask)) {
            if(Mathf.Abs(hit.point.y - movement.transform.position.y) < minDistFromGround) { return; }
        }

        base.Activate(ctx);
    }

    public override void DoAbility()
    {
        movement.rb.drag = 1;
        movement.rb.AddForce(Vector3.down * speed, ForceMode.Impulse);
        _effect.IsSlamming = true;
        timePassed = 0;
    }

    public override void UpdateAbility(float dt)
    {
        base.UpdateAbility(dt);
        if(currentAmount < 1) { return; }

        //if (action.IsPressed()) {
        //    timePassed += dt;
        //}else { timePassed = 0; }


        //if (timePassed > 0.25f) {
        //    movement.rb.drag = graph.Evaluate(timePassed) * 2;
        //}
        //else {
        //    movement.rb.drag = 1;
        //}

    }

}
