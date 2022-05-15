using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Quest.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] SODialogue currentDialogue;
        DialogueNode currentNode = null;
        bool isChoosing = false;

        private void Awake()
        {
            currentNode = currentDialogue.GetRootNode();
        }
        public bool IsChoosing()
        {
            return isChoosing;
        }

        public string GetText()
        {
            if(currentNode == null)
            {
                return "";
            }
            return currentNode.GetText();
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return currentDialogue.GetPlayerChildren(currentNode);
        }

        public void NextDialogue()
        {
            int numberOfPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();
            if(numberOfPlayerResponses > 0)
            {
                isChoosing = true;
                return;
            }

            DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
            int randomIndex = Random.Range(0, children.Count());
            currentNode = children[randomIndex];
        }
        public bool HasNextDialogue()
        {            
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;
        }
    }
}