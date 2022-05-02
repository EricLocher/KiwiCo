using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(SkillController))]
public class SkillTreeEditor : Editor
{
    SkillController controller;
    SkillTree skillTree;
    SkillTreeWindow window;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Open Window")) {
            window = (SkillTreeWindow)EditorWindow.CreateInstance(typeof(SkillTreeWindow));
            window.Init(skillTree, controller.windowStyleSheet);
            window.CreateWindow();
        }
    }

    void OnEnable()
    {
        controller = (SkillController)target;
        skillTree = controller.skillTree;
    }

}
