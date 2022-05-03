using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] SOItem[] items;

    private void Start()
    {
        foreach (var item in items)
        {
            Debug.Log($"Has item: {item.GetName()}");
        }
    }
}
