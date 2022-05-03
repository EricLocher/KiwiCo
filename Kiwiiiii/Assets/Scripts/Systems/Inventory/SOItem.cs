using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Utilities/Inventory/Item")]

public class SOItem : ScriptableObject
{
    public string itemName;
    public string description;

    public int inventoryIndex;
    public int maxAmount;

    public string GetName()
    {
        return itemName;
    }
}