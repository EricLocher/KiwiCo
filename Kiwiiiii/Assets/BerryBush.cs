using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBush : MonoBehaviour, IInteractable
{
    public void Interact(PlayerController controller)
    {       
        controller.playerStats.berriesInInventory++;
        Debug.Log(controller.playerStats.berriesInInventory);
        Destroy(gameObject);
        //TODO: Make a modular system for items on player
    }
}
