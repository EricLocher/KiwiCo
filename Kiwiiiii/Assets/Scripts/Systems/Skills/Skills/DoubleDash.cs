using UnityEngine;

[CreateAssetMenu(fileName = "DoubleDash", menuName = "Utilities/Skills/DoubleDash")]
public class DoubleDash : Skill
{
    public override void LevelUp()
    {
        pointsToLevelUp--;
        if(pointsToLevelUp == 0) {
            Debug.Log("Leveled up DoubleDash");
        }
    }
}
