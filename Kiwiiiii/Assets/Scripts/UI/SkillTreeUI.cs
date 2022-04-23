using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeUI : MonoBehaviour
{
    [SerializeField] GameObject skillPrefab;
    [SerializeField] GameObject skillPanelPrefab;
    [SerializeField] GameObject skillPanel;

    SkillController skillTree;

    List<GameObject> _skillPanels;

    void Awake()
    {
        skillTree = GetComponent<SkillController>();
        skillTree.Init();
    }
    void Start()
    {
        SkillTree _tree = skillTree.skillTree;

        _skillPanels = new List<GameObject>();

        for (int i = 0; i <= _tree.depth; i++) {
            _skillPanels.Add(Instantiate(skillPanelPrefab, skillPanel.transform));
        }

        foreach (Skill skill in _tree.skills) {
            SkillButton _skill = Instantiate(skillPrefab, _skillPanels[skill.index].transform).GetComponent<SkillButton>();
            _skill.skill = skill;
            _skill.Init();
        }

    }

}
