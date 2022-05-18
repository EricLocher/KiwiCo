using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Attacks))]
public class AttackEditor : Editor
{
    Attacks attacks;

    List<Editor> editors = new List<Editor>();

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DrawButtons();
        DrawInspectors();
        PrefabUtility.RecordPrefabInstancePropertyModifications(attacks);
    }

    void OnSceneGUI()
    {
        DrawHandles();
    }

    void DrawButtons()
    {
        if(GUILayout.Button("Add New Attack")) {
            AttackCone _attack = (AttackCone)CreateInstance(typeof(AttackCone));
            _attack.origin = attacks.transform;
            attacks.AddAttack(_attack);
            
            CreateEditors();
        }
    }

    void DrawInspectors()
    {
        if (editors.Count == 0) { return; }

        EditorGUILayout.BeginVertical();

        for (int i = 0; i < editors.Count; i++) {
            editors[i].OnInspectorGUI();
        }

        EditorGUILayout.EndVertical();
    }

    void DrawHandles()
    {
        foreach (AttackCone attack in attacks.attacks) {
            Handles.color = Color.magenta;

            Vector3 viewAngleA = attack.DirFromAngle(-attack.angle / 2);
            Vector3 viewAngleB = attack.DirFromAngle(attack.angle / 2);
            Handles.DrawWireArc(attack.origin.position, Vector3.up, viewAngleA, attack.angle, attack.radius);
            Handles.DrawLine(attack.origin.position, attack.origin.position + viewAngleA * attack.radius);
            Handles.DrawLine(attack.origin.position, attack.origin.position + viewAngleB * attack.radius);

        }
    }

    void CreateEditors()
    {
        editors.Clear();
        foreach (AttackCone attack in attacks.attacks) {
            editors.Add(CreateEditor(attack));
        }
    }

    private void OnEnable()
    {
        attacks = (Attacks)target;
        CreateEditors();
    }

}
