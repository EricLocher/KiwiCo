using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;


namespace Quest.Dialogue.Editor
{
    public class DialogueModifiyingProcessor : UnityEditor.AssetModificationProcessor
    {
        private static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath)
        {
            SODialogue dialogue = AssetDatabase.LoadMainAssetAtPath(sourcePath) as SODialogue;
            if (dialogue == null)
            {
                return AssetMoveResult.DidNotMove;
            }
            if(Path.GetDirectoryName(sourcePath) != Path.GetDirectoryName(destinationPath))
            {
                return AssetMoveResult.DidNotMove;
            }
            dialogue.name = Path.GetFileNameWithoutExtension(destinationPath);
            return AssetMoveResult.DidNotMove;
        }
    }
}