using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractController : MonoBehaviour
{

    List<GameObject> interactables = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<IInteractable>(out IInteractable interactable);
        
        if(interactable != null) {
            interactables.Add(other.gameObject);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        other.TryGetComponent<IInteractable>(out IInteractable interactable);

        if (interactable != null) {
            interactables.Remove(other.gameObject);
        }

    }

    public void Interact(InputAction.CallbackContext value)
    {
        if(interactables.Count == 0) { return; }
        if(!value.performed) { return; }
       


        GameObject closestInteractable = interactables[0];
        float dist = Vector3.Distance(transform.position, interactables[0].transform.position);

        for (int i = 1; i < interactables.Count; i++) {
            if(Vector3.Distance(transform.position, interactables[i].transform.position) < dist) {
                closestInteractable = interactables[i];
            }
        }

        closestInteractable.GetComponent<IInteractable>().Interact();


    }


}
