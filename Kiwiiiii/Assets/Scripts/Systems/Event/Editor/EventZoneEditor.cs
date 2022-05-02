using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EventZone))]
public class EventZoneEditor : Editor
{
    EventZone _target;
    List<Editor> editors = new List<Editor>();
    bool showEditors;
    bool addEvent = false;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        _target.CheckDependency();

        float sliderVal = EditorGUILayout.Slider(_target.zoneCollider.radius, 10, 100);
        if (_target.zoneCollider.radius != sliderVal) {
            Undo.RecordObject(_target, "Updated Radius");

            _target.zoneCollider.radius = sliderVal;
        }

        DrawButtons();
        DrawInspectors();
    }

    public void OnSceneGUI()
    {
        Handles.color = Color.red;
        Handles.DrawWireArc(_target.transform.position, Vector3.up, Vector3.forward, 360, _target.zoneCollider.radius);
    }

    void DrawButtons()
    {
        addEvent = EditorGUILayout.Foldout(addEvent, "Add Event", true);

        if (addEvent) {

            if (GUILayout.Button("Add Enemy Event")) {
                _target.AddEvent(new EnemyEvent());
                CreateEditors();
            }

            if (GUILayout.Button("Add NPC Event")) {
                _target.AddEvent(new NPCEvent());
                CreateEditors();
            }

            EditorGUILayout.Space(10);
        }
    }

    void DrawInspectors()
    {
        if (editors.Count == 0) { return; }

        showEditors = EditorGUILayout.Foldout(showEditors, "Events", true);

        if (showEditors) {
            EditorGUILayout.BeginVertical("Box");

            for (int i = 0; i < editors.Count; i++) {
                editors[i].OnInspectorGUI();
                if (GUILayout.Button("Remove Event")) {
                    _target.RemoveEvent(i);
                    CreateEditors();
                }
                EditorGUILayout.Space();
            }

            EditorGUILayout.EndVertical();
        }
    }

    void CreateEditors()
    {
        editors.Clear();
        foreach (GameEvent GameEvent in _target.events) {
            editors.Add(CreateEditor(GameEvent));
        }
    }

    private void OnEnable()
    {
        _target = (EventZone)target;
        CreateEditors();
    }

}
