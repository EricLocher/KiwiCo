using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillTree
{
    public List<Skill> startNodes;
    public List<Skill> skills;

    [HideInInspector] public int depth = 0;

    public void CalculateTree()
    {
        foreach (Skill skill in startNodes) {
            int _index = skill.calculateIndex(0);
            if(_index >= depth) { depth = _index; }
        }
        
    }
}
