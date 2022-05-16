using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiConversant : MonoBehaviour, IInteractable
{
    [SerializeField] string npcName;

    public string GetName()
    {
        return npcName;
    }

    public void Interact(PlayerController controller)
    {
        //TODO: Make dialogue pop up when player presses E
    }
}