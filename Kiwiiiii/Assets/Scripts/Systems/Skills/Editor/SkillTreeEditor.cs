using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillController))]
public class SkillTreeEditor : Editor
{
    SkillController controller;
    SkillTree skillTree;
    SkillTreeWindow window;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Open Window")) {
            window = (SkillTreeWindow)CreateInstance(typeof(SkillTreeWindow));
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
