using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HotbarBehavior : MonoBehaviour
{
    //This script is temporary
    // AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
    // AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAUIUIUIOOOOOOOOOOOOOOEEEEEEEEEEEEEEEOOOOOOOOOOOOOOEIEIEIOOO
    // Det var en g�ng en kod som borde funka
    // Men den ville inte det f�r den var Satans egna barn
    // Ful �r den ocks�
    // KAPISHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH
    // Hj�rnmush
    // moschscshchschschschshcshshcshchschshcshchshchschshccshscchschschshcshchsch

    private PlayerController stats;
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
}
