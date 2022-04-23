using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Skill : ScriptableObject
{
    [HideInInspector] public int index = 0;

    public string skillName;
    [TextArea]
    public string skillDescription;
    public int pointsToLevelUp;
    public List<Skill> connectsTo;


    public void LevelUp()
    {
        if(pointsToLevelUp <= 0) { return; }
        pointsToLevelUp--;
        Action();
    }

    public abstract void Action();

    public override string ToString() => skillName;

    public int calculateIndex(int currentIndex)
    {
        index = currentIndex;
        foreach (Skill connectedSkill in connectsTo) {
            int _index = connectedSkill.calculateIndex(index + 1);
            if(_index >= currentIndex) { currentIndex = _index; }
        }
        
        return currentIndex;

    }

}
