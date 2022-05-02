using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PatrolSpots))]
public class PatrolEditor : Editor
{
    PatrolSpots patrolSpots;

    void OnSceneGUI()
    {
        Input();
        Draw();
    }

    void Input()
    {
        Event guiEvent = Event.current;
        Vector3 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift) {
            Undo.RecordObject(patrolSpots, "Add Spot");
            patrolSpots.AddSpot(mousePos);
        }
    }

    void Draw()
    {
        if(patrolSpots.spots.Count == 0) { return; }
        Handles.color = Color.yellow;

        for (int i = 0; i < patrolSpots.spots.Count; i++) {
            Vector3 newPos = Handles.FreeMoveHandle(patrolSpots.spots[i], Quaternion.identity, .5f, Vector2.zero, Handles.CylinderHandleCap);

            if (patrolSpots.spots[i] != newPos) {
                Undo.RecordObject(patrolSpots, "Move Spot");
                patrolSpots.MoveSpot(i, newPos);
            }
        }

    }

    private void OnEnable()
    {
        patrolSpots = (PatrolSpots)target;
    }

}
