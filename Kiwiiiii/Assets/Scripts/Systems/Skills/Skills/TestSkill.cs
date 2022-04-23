using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestSkill", menuName = "Utilities/Skills/TestSkill")]
public class TestSkill : Skill
{
    public override void Action()
    {
        Debug.Log("Leveled up Testskill");
    }
}
