using UnityEngine;

[CreateAssetMenu(fileName = "Slam", menuName = "Utilities/Abilities/Slam")]
public class Slam : Ability
{
    [SerializeField] SlamEffect effect;
    SlamEffect _effect;
    public float slamForce = 30;

    protected override void InitAbility()
    {
        _effect = Instantiate(effect, movement.transform);
        _effect.OnCreate(movement);
    }

    public override void DoAbility()
    {
        movement.rb.AddForce(Vector3.down * slamForce, ForceMode.Impulse);

        _effect.IsSlamming = true;
    }
}
