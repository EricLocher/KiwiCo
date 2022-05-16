using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;

namespace Quest.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        SODialogue selectedDialogue = null;
        [NonSerialized] GUIStyle npcNodeStyle;
        [NonSerialized] GUIStyle playerNodeStyle;
        [NonSerialized] DialogueNode draggingNode = null;
        [NonSerialized] Vector2 draggingOffset;
        [NonSerialized] DialogueNode creatingNode = null;
        [NonSerialized] DialogueNode deletingNode = null;
        [NonSerialized] DialogueNode linkingParentNode = null;
        Vector2 scrollPosition;
        [NonSerialized] bool draggingCanvas = false;
        [NonSerialized] Vector2 draggingCanvasOffset;

        const float canvasSize = 4000;
        const float backgroundSize = 50;

        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }

        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            SODialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as SODialogue;
            if(dialogue != null)
            {
                ShowEditorWindow();
                return true;
            }

            return false;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;
            npcNodeStyle = new GUIStyle();
            npcNodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            npcNodeStyle.padding = new RectOffset(30, 30, 30, 30);
            npcNodeStyle.border = new RectOffset(12, 12, 12, 12);

            Selection.selectionChanged += OnSelectionChanged;
            playerNodeStyle = new GUIStyle();
            playerNodeStyle.normal.background = EditorGUIUtility.Load("node2") as Texture2D;
            playerNodeStyle.padding = new RectOffset(30, 30, 30, 30);
            playerNodeStyle.border = new RectOffset(12, 12, 12, 12);
        }

        private void OnSelectionChanged()
        {
            SODialogue newDialogue = Selection.activeObject as SODialogue;
            if(newDialogue != null)
            {
                selectedDialogue = newDialogue;
                Repaint();
            }
        }

        private void OnGUI()
        {
            if (selectedDialogue != null)
            {
                ProcessEvents();

                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                Rect canvas = GUILayoutUtility.GetRect(canvasSize, canvasSize);
                Texture2D backgroundTexture = Resources.Load("background") as Texture2D;
                Rect texCoords = new Rect(0, 0, canvasSize / backgroundSize, canvasSize / backgroundSize);
                GUI.DrawTextureWithTexCoords(canvas, backgroundTexture, texCoords);

                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawConnections(node);
                }
                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawNodeOnGUI(node);
                }

                EditorGUILayout.EndScrollView();

                if (creatingNode != null)
                {
                    selectedDialogue.CreateNode(creatingNode);
                    creatingNode = null;
                }
                if (deletingNode != null)
                {
                    selectedDialogue.DeleteNode(deletingNode);
                    deletingNode = null;
                }
            }
        }

        private void ProcessEvents()
        {
            if (Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                draggingNode = GetNodePoint(Event.current.mousePosition + scrollPosition);
                if(draggingNode != null)
                {
                    draggingOffset = draggingNode.GetRect().position - Event.current.mousePosition;
                    Selection.activeObject = draggingNode;
                }
                else
                {
                    draggingCanvas = true;
                    draggingCanvasOffset = Event.current.mousePosition + scrollPosition;
                    Selection.activeObject = selectedDialogue;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                draggingNode.SetRectPos(Event.current.mousePosition + draggingOffset);

                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseDrag && draggingCanvas)
            {
                scrollPosition = draggingCanvasOffset - Event.current.mousePosition;

                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }
            else if (Event.current.type == EventType.MouseUp && draggingCanvas)
            {
                draggingCanvas = false;
            }

        }

        private void DrawNodeOnGUI(DialogueNode node)
        {
            GUIStyle style = npcNodeStyle;
            if(node.PlayerSpeaking())
            {
                style = playerNodeStyle;
            }
            GUILayout.BeginArea(node.GetRect(), style);

            node.SetText(EditorGUILayout.TextField(node.GetText()));

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                creatingNode = node;
            }
            DrawLinkButtons(node);
            if (GUILayout.Button("-"))
            {
                deletingNode = node;
            }
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private void DrawLinkButtons(DialogueNode node)
        {
            if (linkingParentNode == null)
            {
                if (GUILayout.Button("link"))
                {
                    linkingParentNode = node;
                }
            }
            else if (linkingParentNode == node)
            {
                if (GUILayout.Button("cancel"))
                {
                    linkingParentNode = null;
                }
            }
            else if(linkingParentNode.GetChildren().Contains(node.name))
            {
                if (GUILayout.Button("unlink"))
                {
                    linkingParentNode.RemoveChild(node.name);
                    linkingParentNode = null;
                }
            }
            else
            {
                if (GUILayout.Button("link"))
                {
                    linkingParentNode.AddChild(node.name);
                    linkingParentNode = null;
                }
            }
        }

        private void DrawConnections(DialogueNode node)
        {
            Vector3 startPos = new Vector2(node.GetRect().xMax, node.GetRect().center.y);

            foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(node))
            {
                Vector3 endPos = new Vector2(childNode.GetRect().xMin, childNode.GetRect().center.y);
                Vector3 controlPointOffset = endPos - startPos;
                controlPointOffset.y = 0;
                controlPointOffset.x *= 0.8f;
                Handles.DrawBezier
                    (startPos, endPos, 
                    startPos + controlPointOffset, 
                    endPos - controlPointOffset, 
                    Color.white, null, 4f);
            }
        }

        private DialogueNode GetNodePoint(Vector2 mousePosition)
        {
            DialogueNode nodeFound = null;
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                if(node.GetRect().Contains(mousePosition))
                {
                    nodeFound = node;
                }
            }
            return nodeFound;
        }
    }
}