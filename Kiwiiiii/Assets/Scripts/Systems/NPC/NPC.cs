using UnityEngine;
using UnityEngine.VFX;

public class NPC : MonoBehaviour, IInteractable
{
    public Animator animator;
    public Dialogue dialogue;
    public DialogueManager dialogueManager;
    [SerializeField] VisualEffect vfx;
    [SerializeField] Rigidbody character;

    void OnEnable()
    {
        vfx.Play();
    }

    public void Interact(PlayerController controller)
    {
        animator.SetBool("IsTalking", true);

        if (!dialogueManager.IsOpen) 
        {
            dialogueManager.StartDialogue(dialogue, StopTalking);
            character.constraints = RigidbodyConstraints.FreezeAll;
        }
        else 
        {
            dialogueManager.DisplayNextSentance();
        }
    }

    public void StopTalking()
    {
        animator.SetBool("IsTalking", false);
        character.constraints = RigidbodyConstraints.None;
    }


}
