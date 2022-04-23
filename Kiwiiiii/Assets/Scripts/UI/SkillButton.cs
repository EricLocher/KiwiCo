using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public TMP_Text skillName, reqPoints;
    public Transform previous, next;
    public Skill skill;

    Button btn;

    public void Init()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(skill.LevelUp);
        btn.onClick.AddListener(OnClick);
        skillName.text = skill.skillName;
        reqPoints.text = $"{skill.pointsToLevelUp}";

        if(skill.pointsToLevelUp == 0) { btn.interactable = false; }
    }

    void OnClick()
    {
        reqPoints.text = $"{skill.pointsToLevelUp}";
        if(skill.pointsToLevelUp == 0) { btn.interactable = false; }
    }


}
