using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{

    public Dialogue dialogue;

    public DialogueManager dialogueManager;

    public void Interact()
    {
        if (!dialogueManager.IsOpen) 
        {
            dialogueManager.StartDialogue(dialogue);
        }
        else 
        {
            dialogueManager.DisplayNextSentance();
        }
    }

}
