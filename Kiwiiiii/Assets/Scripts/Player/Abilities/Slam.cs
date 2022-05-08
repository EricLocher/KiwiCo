using UnityEngine;

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

    protected override void InitAbility()
    {
        _effect = Instantiate(effect, movement.transform);
        _effect.OnCreate(movement, force, radius, damage);
    }

    public override void DoAbility()
    {
        movement.rb.AddForce(Vector3.down * speed, ForceMode.Impulse);

        _effect.IsSlamming = true;
    }
}
