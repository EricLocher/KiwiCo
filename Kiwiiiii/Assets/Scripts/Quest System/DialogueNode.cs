using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Quest.Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        //for more characters involved this bool could be an enum
        [SerializeField] private bool playerSpeaking = false;
        [SerializeField] private string text;
        [SerializeField] private List<string> children = new List<string>();
        [SerializeField] private Rect rect = new Rect(0, 0, 200, 100);
        [SerializeField] private string onExitAction;
        [SerializeField] private string onEnterAction;

        public string GetOnEnterAction()
        {
            return onEnterAction;
        }
        public string GetOnExitAction()
        {
            return onExitAction;
        }


        public Rect GetRect()
        {
            return rect;
        }

        public string GetText()
        {
            return text;
        }

        public List<string> GetChildren()
        {
            return children;
        }
        public bool PlayerSpeaking()
        {
            return playerSpeaking;
        }
#if UNITY_EDITOR
        public void SetRectPos(Vector2 newPos)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = newPos;
            EditorUtility.SetDirty(this);
        }

        public void SetText(string newText)
        {
            if(newText != text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText;
                EditorUtility.SetDirty(this);
            }
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            children.Add(childID);
            EditorUtility.SetDirty(this);
        }
        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
        }
        public void SetPlayerSpeaking(bool newPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            playerSpeaking = newPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }
#endif
    }
}