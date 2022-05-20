using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Attacks))]
public class AttackEditor : Editor
{
    Attacks attacks;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        foreach (AttackCone attack in attacks.attacks) {
                attack.origin = attacks.transform;
        }
    }

    void OnSceneGUI()
    {
        DrawHandles();
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

    private void OnEnable()
    {
        attacks = (Attacks)target;
    }

}
