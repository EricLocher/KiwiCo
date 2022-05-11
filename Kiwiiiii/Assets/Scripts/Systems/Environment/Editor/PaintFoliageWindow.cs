using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PaintFoliageWindow : EditorWindow
{
    [SerializeField] List<GameObject> palette = new List<GameObject>();
    [SerializeField] int paletteIndex;

    string path = "Assets/Models/Environment/Prefabs";
    bool paintMode = false;

    [MenuItem("Window/Paint Foliage")]
    static void ShowWindow()
    {
        GetWindow(typeof(PaintFoliageWindow));
    }

    void OnGUI()
    {
        paintMode = GUILayout.Toggle(paintMode, "Start painting", "Button", GUILayout.Height(60f));

        List<GUIContent> paletteIcons = new List<GUIContent>();
        foreach (GameObject prefab in palette)
        {
            Texture2D texture = AssetPreview.GetAssetPreview(prefab);
            paletteIcons.Add(new GUIContent(texture));
        }

        paletteIndex = GUILayout.SelectionGrid(paletteIndex, paletteIcons.ToArray(), 6);
    }

    void OnSceneGUI(SceneView sceneView)
    {
        if (paintMode)
        {
            //DisplayVisualHelp();
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

    void DisplayVisualHelp() { }

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