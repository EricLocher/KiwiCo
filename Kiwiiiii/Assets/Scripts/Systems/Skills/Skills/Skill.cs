using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Skill : TreeNode
{
    [Header("Info")]
    public int pointsToLevelUp;
    public string skillName;
    [TextArea] public string skillDescription;

    public override string ToString() => skillName;
    public abstract void Action();
    public void LevelUp()
    {
        if(pointsToLevelUp <= 0) { return; }
        pointsToLevelUp--;
        Action();
    }
}
