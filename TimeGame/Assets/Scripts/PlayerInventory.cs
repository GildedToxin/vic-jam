using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void AddItem(InventoryItem inventoryItem)
    {
        items.Add(inventoryItem);
    }
    public void RemoveItem(InventoryItem inventoryItem)
    {
        if (items.Contains(inventoryItem))
        {
            items.Remove(inventoryItem);
        }
    }

    public bool DoesPlayerHaveItem(InventoryItem inventoryItem)
    {
        if (items.Contains(inventoryItem))
        {
            return true;
        }

        return false;
    }
}

public enum InventoryItem
{
    None,
    Gear,
    Book,
}
