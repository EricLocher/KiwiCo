using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractController : MonoBehaviour
{
    public List<GameObject> interactables = new List<GameObject>();
    public GameObject interactNotice;

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out IInteractable interactable);
        
        if(interactable != null) {
            interactables.Add(other.gameObject);
            interactNotice.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.TryGetComponent(out IInteractable interactable);

        //if (interactable != null) {
        //    interactables.Remove(other.gameObject);
        //    interactNotice.SetActive(false);
        //    //if (interactable is NPC)
        //    //{
        //    //    other.GetComponent<NPC>().dialogueManager.EndDialogue();
        //    //}
        //}
    }

    public void Interact(InputAction.CallbackContext value)
    {
        print("Eri");
        if(interactables.Count == 0) { return; }
        if (!value.performed) { return; }

        GameObject closestInteractable = interactables[0];
        float dist = Vector3.Distance(transform.position, interactables[0].transform.position);

        for (int i = 1; i < interactables.Count; i++) 
        {
            if(Vector3.Distance(transform.position, interactables[i].transform.position) < dist) 
            {
                closestInteractable = interactables[i];
            }
        }
        Debug.Log(closestInteractable);
        closestInteractable.GetComponent<IInteractable>().Interact();
    }
}
