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
            if(skill.previousSkill == null) { continue; }

            int _index = skill.index;
            if(depth >= _index) { continue; }
            depth = _index;
        }
    }
}
