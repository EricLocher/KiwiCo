using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Skill : ScriptableObject
{
    [Header("Info")]
    public int pointsToLevelUp;
    public string skillName;
    [TextArea] public string skillDescription;

    [HideInInspector] public int index = 0;
    [HideInInspector] public List<Skill> parents;
    public List<Skill> connectsTo;

    public override string ToString() => skillName;
    public abstract void Action();
    public void LevelUp()
    {
        if(pointsToLevelUp <= 0) { return; }
        pointsToLevelUp--;
        Action();
    }

    #region Calculate Tree

    public int calculateChildIndices(int currentIndex)
    {
        index = currentIndex;
        foreach (Skill connectedSkill in connectsTo) {
            int _index = connectedSkill.calculateChildIndices(index + 1);
            if(_index >= currentIndex) { currentIndex = _index; }
        }
        
        return currentIndex;
    }

    public void AssignParent()
    {
        foreach (Skill connectedSkill in connectsTo) {
            connectedSkill.parents.Add(this);
            connectedSkill.AssignParent();
        }
    }

    #endregion
}
