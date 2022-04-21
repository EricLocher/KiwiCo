using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeUI : MonoBehaviour
{
    [SerializeField] GameObject skillPrefab;
    [SerializeField] GameObject skillPanel;
    [SerializeField] GameObject skillPanelPrefab;

    SkillController skillTree;

    List<GameObject> _skillPanels;

    void Awake()
    {
        skillTree = GetComponent<SkillController>();
        skillTree.Init();
    }
    void Start()
    {
        List<Skill> skills = skillTree.skillTree.skills;

        _skillPanels = new List<GameObject>();

        print(skillTree.skillTree.depth);

        for (int i = 0; i < skillTree.skillTree.depth + 1; i++) {
            var _temp = Instantiate(skillPanelPrefab, skillPanel.transform);
            _temp.name = $"Skills ({i})";
            _skillPanels.Add(_temp);
        }

        for (int i = 0; i < skills.Count; i++) {
            var _temp = Instantiate(skillPrefab, _skillPanels[skills[i].index].transform);
            _temp.name = skills[i].skillName;
        }
    }

}
