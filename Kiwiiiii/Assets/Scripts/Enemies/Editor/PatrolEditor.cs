using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PatrolSpots))]
public class PatrolEditor : Editor
{
    PatrolSpots patrolSpots;
    bool showSpots = true;
    int lastMovedIndex = 0;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Use Circle"))
        {
            Undo.RecordObject(target, "Updated Behaviour");
            patrolSpots.circlePos = patrolSpots.transform.position;
            patrolSpots.circle = !patrolSpots.circle;
        }


        if (!patrolSpots.circle)
        {

            if (GUILayout.Button("Show Spots"))
            {
                showSpots = !showSpots;
            }

            if (showSpots)
            {
                if (GUILayout.Button("Add Spot"))
                {
                    Undo.RecordObject(target, "Added New Spot");
                    patrolSpots.AddSpot(new Vector3(0, 10, 0));
                }

                if (GUILayout.Button("Remove Spot"))
                {
                    Undo.RecordObject(target, "Removed Spot");
                    patrolSpots.RemoveSpot(lastMovedIndex);
                    lastMovedIndex = 0;
                }

                if (GUILayout.Button("Raycast Spot to ground"))
                {
                    Undo.RecordObject(target, "Grounded Spot");
                    patrolSpots.RayTraceToGround(lastMovedIndex);
                }


                if (GUILayout.Button("Raycast All to ground"))
                {
                    Undo.RecordObject(target, "Grounded All Spots");
                    for (int i = 0; i < patrolSpots.spots.Count; i++)
                    {
                        patrolSpots.RayTraceToGround(i);
                    }
                }
            }
        }
        else
        {
            EditorGUILayout.LabelField("Patrol Radius", EditorStyles.boldLabel);
            float sliderVal = EditorGUILayout.Slider(patrolSpots.circleRadius, 10, 100);
            if (patrolSpots.circleRadius != sliderVal)
            {
                Undo.RecordObject(patrolSpots, "Updated Radius");

                patrolSpots.circleRadius = sliderVal;
            }
        }
    }

    void OnSceneGUI()
    {
        Draw();
    }

    void Draw()
    {
        if (patrolSpots.circle)
        {
            Handles.color = Color.red;
            Handles.DrawWireArc(patrolSpots.circlePos, Vector3.up, Vector3.forward, 360, patrolSpots.circleRadius);
            return;
        }


        if (!showSpots) { return; }

        if (patrolSpots.spots.Count == 0) { return; }


        for (int i = 0; i < patrolSpots.spots.Count; i++)
        {
            Handles.color = Color.yellow;
            Handles.DrawLine(patrolSpots.transform.position, patrolSpots[i]);

            if (i == lastMovedIndex) { Handles.color = Color.red; }
            else { Handles.color = Color.green; }
            Vector3 newPos = Handles.FreeMoveHandle(patrolSpots[i], Quaternion.identity, .5f, Vector2.zero, Handles.CylinderHandleCap);

            if (patrolSpots[i] != newPos)
            {
                Undo.RecordObject(patrolSpots, "Move Spot");
                patrolSpots.MoveSpot(i, newPos);
                lastMovedIndex = i;
            }
        }

    }

    private void OnEnable()
    {
        patrolSpots = (PatrolSpots)target;
    }

}
