using UnityEngine;

[CreateAssetMenu(fileName = "Slam", menuName = "Utilities/Abilities/Slam")]
public class Slam : Ability
{
    public float slamForce = 30;

    public override void DoAbility(Rigidbody rb)
    {
        rb.AddForce(Vector3.down * slamForce, ForceMode.Impulse);
    }
}
