using UnityEngine;

[CreateAssetMenu(fileName = "Slam", menuName = "Utilities/Abilities/Slam")]
public class Slam : Ability
{
    SlamEffect _effect;
    [Header("Slam Specific Settings")]
    [SerializeField] SlamEffect effect;
    public float Speed = 30;
    public float force = 10;
    public float radius = 6;

    protected override void InitAbility()
    {
        _effect = Instantiate(effect, movement.transform);
        _effect.OnCreate(movement, force, radius);
    }

    public override void DoAbility()
    {
        movement.rb.AddForce(Vector3.down * Speed, ForceMode.Impulse);

        _effect.IsSlamming = true;
    }
}
