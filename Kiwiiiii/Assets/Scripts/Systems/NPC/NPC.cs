using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{

    public Dialogue dialogue;

    public DialogueManager dialogueManager;

    public void Interact(PlayerController controller)
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
