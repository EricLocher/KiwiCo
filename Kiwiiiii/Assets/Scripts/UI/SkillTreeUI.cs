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


    }

    


}


