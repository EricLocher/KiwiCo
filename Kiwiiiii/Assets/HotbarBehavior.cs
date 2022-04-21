using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HotbarBehavior : MonoBehaviour
{
    //This script is temporary

    public TextMeshProUGUI amount;
    public void Weapon(PlayerController controller)
    {

    }
    public void Items(PlayerController controller)
    {
        amount.text = controller.playerStats.berriesInInventory.ToString();
    }
}
