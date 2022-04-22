using UnityEngine;

[CreateAssetMenu(fileName = "HealthUp", menuName = "Utilities/Skills/HealthUp")]
public class HealthUp : Skill
{
    public override void LevelUp()
    {
        pointsToLevelUp--;
        if(pointsToLevelUp == 0) {
            Debug.Log("Leveled up HealthUp");
        }
    }
}
