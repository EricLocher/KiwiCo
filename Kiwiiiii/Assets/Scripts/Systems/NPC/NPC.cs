using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    public Animator animator;
    public Dialogue dialogue;

    public DialogueManager dialogueManager;

    public void Interact(PlayerController controller)
    {
        animator.SetBool("IsTalking", true);

        if (!dialogueManager.IsOpen) 
        {
            dialogueManager.StartDialogue(dialogue, StopTalking);
        }
        else 
        {
            dialogueManager.DisplayNextSentance();
        }
    }

    public void StopTalking()
    {
        animator.SetBool("IsTalking", false);
    }


}
