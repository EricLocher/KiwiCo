using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest.Dialogue
{
    public class aiConversant : MonoBehaviour, IInteractable
    {
        [SerializeField] string npcName;
        PlayerConversant playerConversant;
        [SerializeField] SODialogue dialogue;
        aiConversant aiSpeaker;

        private void Awake()
        {
            aiSpeaker = this;
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
        }
        public string GetName()
        {
            return npcName;
        }
        public void Interact()
        {
            //TODO: Make dialogue pop up when player presses E
            playerConversant.StartDialogue(aiSpeaker, dialogue);
            //StartDialogue(aiConversant newConversant, SODialogue newDialogue)
        }
    }
}