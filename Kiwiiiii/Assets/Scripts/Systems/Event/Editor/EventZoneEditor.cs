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
        _target.CheckDependency();

        EditorGUILayout.LabelField("Zone radius", EditorStyles.boldLabel);
        float sliderVal = EditorGUILayout.Slider(_target.zoneCollider.radius, 10, 100);
        if (_target.zoneCollider.radius != sliderVal)
        {
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

        if (addEvent)
        {

            if (GUILayout.Button("Add Enemy Event"))
            {

                _target.AddEvent((GameEvent)CreateInstance(typeof(EnemyEvent)));
                CreateEditors();
            }

            if (GUILayout.Button("Add NPC Event"))
            {

                _target.AddEvent((GameEvent)CreateInstance(typeof(NPCEvent)));
                CreateEditors();
            }

            if (GUILayout.Button("Add Cinematic Event"))
            {
                _target.AddEvent((GameEvent)CreateInstance(typeof(CutsceneEvent)));
                CreateEditors();
            }

            EditorGUILayout.Space(10);
        }
    }

    void DrawInspectors()
    {

        if (editors.Count == 0) { return; }

        showEditors = EditorGUILayout.Foldout(showEditors, "Events", true);

        if (showEditors)
        {
            EditorGUILayout.BeginVertical("Box");

            for (int i = 0; i < editors.Count; i++)
            {

                _target.events[i].Minimized = EditorGUILayout.Foldout(_target.events[i].Minimized, _target.events[i].ToString(), true);

                if (!_target.events[i].Minimized) { continue; }

                editors[i].OnInspectorGUI();

                if (GUILayout.Button("Remove Event"))
                {
                    _target.RemoveEvent(i);
                    CreateEditors();
                }

                EditorGUILayout.BeginHorizontal();

                if (i != 0 && GUILayout.Button("Move Up"))
                {
                    _target.SwapElements(i, i - 1);
                    CreateEditors();
                }

                if (i != (editors.Count - 1) && GUILayout.Button("Move Down"))
                {
                    _target.SwapElements(i, i + 1);
                    CreateEditors();
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }
    }

    void CreateEditors()
    {
        editors.Clear();
        foreach (GameEvent GameEvent in _target.events)
        {
            editors.Add(CreateEditor(GameEvent));
        }
    }

    private void OnEnable()
    {
        _target = (EventZone)target;
        CreateEditors();
    }

}