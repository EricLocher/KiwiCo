using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBerry : MonoBehaviour, IItem
{
    public void Use(PlayerController controller)
    {
        if(controller.playerStats.berriesInInventory == 0)
        { return; }
        controller.health.Heal(50);
        controller.playerStats.berriesInInventory--;
    }
}
