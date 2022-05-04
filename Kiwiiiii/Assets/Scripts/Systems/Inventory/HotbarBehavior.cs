using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HotbarBehavior : MonoBehaviour
{
    //TODO: More intuitive system

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
        amount.text = stats.stats.berriesInInventory.ToString();
    }

    public void UseItem(int index)
    {
        berry.Use(stats);
    }
}
