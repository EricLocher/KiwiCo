using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Dash", menuName = "Utilities/Abilities/Dash")]
public class Dash : Ability
{
    [Header("Dash Specific Settings")]
    public float speed;

    public override void DoAbility()
    {
        Vector3 dir = Camera.main.transform.forward;

        movement.rb.AddForce(dir.normalized * speed, ForceMode.Impulse);
    }
}
