using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    Enemy enemy;

    void OnSceneGUI()
    {
        Draw();
    }

    void Draw()
    {
        Handles.color = Color.red;

        Handles.DrawWireArc(enemy.transform.position, Vector3.up, Vector3.forward, 360, enemy.aggroRadius);
        Vector3 viewAngleA = enemy.DirFromAngle(-enemy.fovAngle / 2);
        Vector3 viewAngleB = enemy.DirFromAngle(enemy.fovAngle / 2);
        Handles.color = Color.yellow;
        Handles.DrawWireArc(enemy.transform.position, Vector3.up, viewAngleA, enemy.fovAngle, enemy.fovRadius);
        Handles.DrawLine(enemy.transform.position, enemy.transform.position + viewAngleA * enemy.fovRadius);
        Handles.DrawLine(enemy.transform.position, enemy.transform.position + viewAngleB * enemy.fovRadius);
    }

    void OnEnable()
    {
        enemy = (Enemy)target;
    }

}
