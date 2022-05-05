using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SlamEffect), true)]
public class SlamEditor : Editor
{
    SlamEffect slam;

    void OnSceneGUI()
    {
        Draw();
    }

    void Draw()
    {
        Handles.color = Color.red;
        Handles.DrawWireArc(slam.transform.position, Vector3.up, Vector3.forward, 360, slam.radius);
    }

    void OnEnable()
    {
        slam = (SlamEffect)target;
    }
}
