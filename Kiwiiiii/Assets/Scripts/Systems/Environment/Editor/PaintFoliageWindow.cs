using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PaintFoliageWindow : EditorWindow
{
    [SerializeField] List<GameObject> palette = new List<GameObject>();
    [SerializeField] int paletteIndex;
    GameObject holder = null;
    GameObject group = null;
    float brushSize = 1f;
    int density = 1;
    Vector3 targetPos = new Vector3();
    Vector2 scrollPosition = new Vector2();
    string path = "Assets/Models/Environment/Prefabs";
    bool paintMode = false;

    [MenuItem("Window/Paint Foliage")]
    static void ShowWindow()
    {
        GetWindow(typeof(PaintFoliageWindow));
    }

    void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        paintMode = GUILayout.Toggle(paintMode, "Start painting", "Button", GUILayout.Height(40f));
        GUILayout.Space(20);

        GUILayout.Label("Select your brush size");
        brushSize = GUILayout.HorizontalSlider(brushSize, 1f, 50f);
        GUILayout.Space(20);

        GUILayout.Label("Select your prefab density");
        density = EditorGUILayout.IntSlider(density, 1, 50);
        GUILayout.Space(20);

        List<GUIContent> paletteIcons = new List<GUIContent>();
        foreach (GameObject prefab in palette)
        {
            paletteIcons.Add(new GUIContent(prefab.name));
        }

        paletteIndex = GUILayout.SelectionGrid(paletteIndex, paletteIcons.ToArray(), 2);

        GUILayout.EndScrollView();
    }

    void OnSceneGUI(SceneView sceneView)
    {
        if (paintMode)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetPos = hit.point;
            }

            HandleSceneViewInputs(targetPos);

            sceneView.Repaint();
        }
    }

    void OnFocus()
    {
        RefreshPalette();


        SceneView.duringSceneGui -= OnSceneGUI;
        SceneView.duringSceneGui += OnSceneGUI;
    }

    void OnDestroy()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    void HandleSceneViewInputs(Vector3 targetPos)
    {
        if (Event.current.type == EventType.Layout)
        {
            HandleUtility.AddDefaultControl(0);
        }

        if (paletteIndex < palette.Count && Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            GameObject prefab = palette[paletteIndex];
            if (holder == null)
            {
                holder = new GameObject("Terrain Holder");
            }

            targetPos = targetPos + Vector3.up * 10;

            GameObject[] amount = new GameObject[density];

            if (group == null)
            {
                foreach (GameObject obj in palette)
                {
                    group = new GameObject(obj.name);
                    group.transform.SetParent(holder.transform);
                }
            }

            for (int i = 0; i < density; i++)
            {
                GameObject gameObject = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

                foreach (Transform child in holder.transform)
                {
                    if (gameObject.name == child.name)
                        gameObject.transform.SetParent(child.transform);
                }

                Vector3 randomPoint = Random.insideUnitCircle * brushSize;
                randomPoint = new Vector3(randomPoint.x, 0, randomPoint.y);

                Vector3 placementPos = targetPos + randomPoint;
                Vector3 placementRot = new Vector3();

                RaycastHit grounded;

                if (Physics.Raycast(placementPos, Vector3.down, out grounded))
                {
                    placementPos = grounded.point;
                    placementRot = grounded.normal;
                }

                gameObject.transform.position = placementPos;
                gameObject.transform.up = placementRot;
                Debug.Log(placementRot);

                Undo.RegisterCreatedObjectUndo(gameObject, "");
            }
        }
    }

    void RefreshPalette()
    {
        palette.Clear();
        string[] search_results = System.IO.Directory.GetFiles(path, "*.prefab", System.IO.SearchOption.AllDirectories);
        foreach (string search in search_results)
        {
            GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath(search, typeof(GameObject));
            palette.Add(prefab);
        }
    }
}