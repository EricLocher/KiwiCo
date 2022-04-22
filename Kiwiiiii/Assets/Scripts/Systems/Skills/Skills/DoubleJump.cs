using UnityEngine;

[CreateAssetMenu(fileName = "DoubleJump", menuName = "Utilities/Skills/DoubleJump")]
public class DoubleJump : Skill
{
    public override void LevelUp()
    {
        pointsToLevelUp--;
        if(pointsToLevelUp == 0) {
            Debug.Log("Leveled up DoubleJump");
        }
    }
}
