using UnityEngine;

[CreateAssetMenu(fileName = "DoubleJump", menuName = "Utilities/Skills/DoubleJump")]
public class DoubleJump : Skill
{
    public override void Action()
    {
        Debug.Log("Leveled up Double Jump");
    }
}
