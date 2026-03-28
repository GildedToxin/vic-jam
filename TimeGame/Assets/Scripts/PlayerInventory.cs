using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public GameObject InventoryUI;
    [HideInInspector] public bool isInventoryOpen = false;
    [SerializeField] private InventoryItemRotation inventoryItemRotation;

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InventoryUI.SetActive(!InventoryUI.activeSelf);
            isInventoryOpen = InventoryUI.activeSelf;
            if (inventoryItemRotation != null)
                inventoryItemRotation.itemToRotate.transform.rotation = inventoryItemRotation.startRotation;
        }
    }
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
