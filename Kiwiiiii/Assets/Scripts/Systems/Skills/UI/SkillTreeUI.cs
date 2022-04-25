using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeUI : MonoBehaviour
{
    [SerializeField] GameObject skillPrefab;
    [SerializeField] GameObject skillPanelPrefab;
    [SerializeField] GameObject skillPanel;

    [SerializeField] Vector2 spacing;

    SkillController skillTree;

    void Awake()
    {
        skillTree = GetComponent<SkillController>();
        skillTree.Init();
    }
    void Start()
    {
        SkillTree _tree = skillTree.skillTree;

        foreach (Skill skill in _tree.nodes) {
            var btn = Instantiate(skillPrefab, skillPanel.transform);
            RectTransform rt = btn.GetComponent(typeof(RectTransform)) as RectTransform;
            rt.anchoredPosition = new Vector2((300 + spacing.x) * skill.x, (-150 - spacing.y) * skill.index);
            btn.name = skill.name;
        }
    }

    


}


