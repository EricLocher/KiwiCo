using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillTree
{
    public List<Skill> skills;

    [HideInInspector] public int depth = 0;

    public void CalculateTree()
    {
        foreach (Skill skill in skills) {
            skill.CalculateIndex();

            if (skill.previousSkill == null || depth >= skill.index) { continue; }
            depth = skill.index;
        }

        skills.Sort();
    }
}
