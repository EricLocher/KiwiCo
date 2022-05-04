using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBerry : MonoBehaviour, IItem
{
    public void Use(PlayerController controller)
    {
        if(controller.stats.berriesInInventory == 0)
        { return; }
        controller.Heal(50);
        controller.stats.berriesInInventory--;
    }
}
