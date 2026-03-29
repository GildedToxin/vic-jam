using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public List<GameObject> itemPrefabs = new List<GameObject>();
    public GameObject InventoryUI;
    [HideInInspector] public bool isInventoryOpen = false;
    [SerializeField] private InventoryItemRotation inventoryItemRotation;
    [SerializeField] private GameObject InventoryObjectRotationPoint;
    public TextMeshProUGUI itemDescriptionText;


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
        
        GameObject itemPrefab = itemPrefabs[(int)inventoryItem - 1];
        GameObject itemInstance = Instantiate(itemPrefab, InventoryObjectRotationPoint.transform);
        itemInstance.GetComponent<RectTransform>().localPosition = InventoryObjectRotationPoint.transform.localPosition;
        UpdateItemDescription(inventoryItem);
    }
    public void RemoveItem(InventoryItem inventoryItem)
    {
        if (items.Contains(inventoryItem))
        {
            items.Remove(inventoryItem);
            UpdateItemDescription(inventoryItem);
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

    private bool UpdateItemDescription(InventoryItem inventoryItem)
    {
        switch (inventoryItem)
        {
            case InventoryItem.Gear:
                itemDescriptionText.text = "A rusty gear. It looks like it could be used to fix something.";
                return true;
            case InventoryItem.Book:
                itemDescriptionText.text = "An old book. The title is faded, but it looks like it could contain important information.";
                return true;
            default:
                itemDescriptionText.text = "";
                return false;
        }
    }
}

public enum InventoryItem
{
    None,
    Gear,
    Book,
}
