using UnityEngine;
public abstract class Skill : ScriptableObject
{
    public Skill previousSkill;

    public string skillName;
    public string skillDescription;
    public int pointsToLevelUp;

    public abstract void LevelUp();
    public void SetPreviousSkill(Skill skill)
    {
        if(skill is NoSkill) { previousSkill = null; return; }
        previousSkill = skill;
    }

    public override string ToString() => skillName;
}
