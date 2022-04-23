using UnityEngine;

[CreateAssetMenu(fileName = "HealthUp", menuName = "Utilities/Skills/HealthUp")]
public class HealthUp : Skill
{
    public override void Action()
    {
        Debug.Log("Leveled up Health Up");
    }
}
