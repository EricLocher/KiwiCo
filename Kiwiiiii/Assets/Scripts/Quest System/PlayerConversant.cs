using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace Quest.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] SODialogue tempDialogue;
        [SerializeField] string playerName;
        Rigidbody playerRb;
        SODialogue currentDialogue;
        aiConversant currentNPCSpeaker = null;
        DialogueNode currentNode = null;
        bool isChoosing = false;

        private void Awake()
        {
            playerRb = GameObject.FindGameObjectWithTag("Character").GetComponent<Rigidbody>();
        }

        //updates the dialogue ui whenever a change has been triggered
        public event Action onDialogueUpdate;

        //IEnumerator Start()
        //{
        //    yield return new WaitForSeconds(2);
        //    StartDialogue(tempDialogue);
        //}

        public void StartDialogue(aiConversant newConversant, SODialogue newDialogue)
        {
            playerRb.constraints = RigidbodyConstraints.FreezeAll;
            { Cursor.visible = true; Cursor.lockState = CursorLockMode.None; }
            Camera.main.GetComponent<CameraController>().FreezeCamera(true);
            currentNPCSpeaker = newConversant;
            currentDialogue = newDialogue;
            currentNode = currentDialogue.GetRootNode();
            TriggerEnterNodeAction();
            onDialogueUpdate();
        }
        public void QuitConversing()
        {
            currentDialogue = null;
            TriggerExitNodeAction();
            currentNode = null;
            isChoosing = false;
            currentNPCSpeaker = null;
            onDialogueUpdate();
            playerRb.constraints = RigidbodyConstraints.None;
            { Cursor.visible = false; Cursor.lockState = CursorLockMode.Locked; }
            Camera.main.GetComponent<CameraController>().FreezeCamera(false);
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }

        public string GetCurrentSpeakerName()
        {
            if(isChoosing)
            { return playerName; }
            else
            {
                return currentNPCSpeaker.GetName();
            }
        }

        public bool CurrentDialogueIsActive()
        {
            return currentDialogue != null;
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

        public void ChoiceSelect(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            TriggerEnterNodeAction();
            isChoosing = false;
            NextDialogue();
        }

        public void NextDialogue()
        {
            int numberOfPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();
            if(numberOfPlayerResponses > 0)
            {
                isChoosing = true;
                TriggerExitNodeAction();
                onDialogueUpdate();
                return;
            }

            DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
            int randomIndex = UnityEngine.Random.Range(0, children.Count());
            TriggerExitNodeAction();
            currentNode = children[randomIndex];
            TriggerEnterNodeAction();
            onDialogueUpdate();
        }

        public bool HasNextDialogue()
        {            
            return currentDialogue.GetAllChildren(currentNode).Count() > 0;
        }

        //Trigger Actions are what you want to happen at a specific dialogue, ex. an enemy attacks, trigger a cutscene etc
        private void TriggerEnterNodeAction()
        {
            if(currentNode != null)
            {
                TriggerAction(currentNode.GetOnEnterAction());
            }
        }
        private void TriggerExitNodeAction()
        {
            if (currentNode != null)
            {
                TriggerAction(currentNode.GetOnExitAction());
            }
        }
        private void TriggerAction(string action)
        {
            if(action == "")
            { return; }

            foreach (DialogueTrigger trigger in currentNPCSpeaker.GetComponents<DialogueTrigger>())
            {
                trigger.Trigger(action);
            }
        }
    }
}