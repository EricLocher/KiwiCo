using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Dash", menuName = "Utilities/Abilities/Dash")]
public class Dash : Ability
{
    public float speed;

    public override void DoAbility(Rigidbody rb)
    {
        Vector3 dir = Camera.main.transform.forward;

        rb.AddForce(dir.normalized * speed, ForceMode.Impulse);
    }
}
