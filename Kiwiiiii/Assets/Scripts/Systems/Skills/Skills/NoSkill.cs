using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSkill : Skill
{
    public override void LevelUp()
    {
        Debug.Log("No skill");
    }

    public override string ToString() => "(Empty)";
}
