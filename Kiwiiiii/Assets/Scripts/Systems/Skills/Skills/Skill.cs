using System;
using UnityEngine;
public abstract class Skill : ScriptableObject, IComparable
{

    public int index = 0;

    public Skill previousSkill;

    public string skillName;
    public string skillDescription;
    public int pointsToLevelUp;

    public abstract void LevelUp();
    public void SetPreviousSkill(Skill skill)
    {
        if (skill is NoSkill) { previousSkill = null; return; }
        previousSkill = skill;
    }

    public void CalculateIndex()
    {
        if (previousSkill == null) { index = 0; return; }

        int _index = 1;
        Skill check = previousSkill;

        while (true) {
            if (check.previousSkill == null) { break; }
            previousSkill = check.previousSkill;
            _index++;
        }

        index = _index;
    }

    public override string ToString() => skillName;

    public int CompareTo(object obj)
    {
        Skill other = obj as Skill;
        return index.CompareTo(other.index);
    }
}
