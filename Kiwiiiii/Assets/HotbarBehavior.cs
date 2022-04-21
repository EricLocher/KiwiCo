using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HotbarBehavior : MonoBehaviour
{
    //This script is temporary
    // AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
    // AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUIUIUIOOOOOOOOOOOOOOEEEEEEEEEEEEEEEOOOOOOOOOOOOOOEIEIEIOOO
    // Det var en gång en kod som borde funka
    // Men den ville inte det för den var Satans egna barn
    // Ful är den också
    // KAPISHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH
    // Hjärnmush
    // moschscshchschschschshcshshcshchschshcshchshchschshccshscchschschshcshchsch

    private PlayerController stats;
    [SerializeField] ItemBerry berry;
    public TextMeshProUGUI amount;
    

    private void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        Items();
    }
  
    public void Weapon()
    {

    }
    public void Items()
    {
       // Debug.Log(stats.berriesInInventory + "yoyoyo man wazzup");
        amount.text = stats.playerStats.berriesInInventory.ToString();
    }

    public void UseItem(int index)
    {
        berry.Use(stats);
    }
}
