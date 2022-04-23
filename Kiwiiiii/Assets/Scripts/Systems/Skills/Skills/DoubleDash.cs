using UnityEngine;

[CreateAssetMenu(fileName = "DoubleDash", menuName = "Utilities/Skills/DoubleDash")]
public class DoubleDash : Skill
{
    public override void Action()
    {
        Debug.Log("Leveled up Double Dash");
    }
}
