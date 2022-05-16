using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FOV), true)]
public class FOVEditor : Editor
{
    FOV fov;

    void OnSceneGUI()
    {
        Draw();
    }

    void Draw()
    {
        //Inner radius
        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.innerRadius);

        //Outer radius
        Handles.color = new Color(1, 0.65f, 0);
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.outerRadius);

        //FOV Cone
        Handles.color = Color.yellow;
        Vector3 viewAngleA = fov.DirFromAngle(-fov.fovAngle / 2);
        Vector3 viewAngleB = fov.DirFromAngle(fov.fovAngle / 2);
        Handles.DrawWireArc(fov.transform.position, Vector3.up, viewAngleA, fov.fovAngle, fov.fovRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.fovRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.fovRadius);
    }

    void OnEnable()
    {
        fov = (FOV)target;
    }
}
