using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public List<GameObject> itemPrefabs = new List<GameObject>();
    public GameObject InventoryUI;
    [HideInInspector] public bool isInventoryOpen = false;
    [SerializeField] private InventoryItemRotation inventoryItemRotation;
    [SerializeField] private List<GameObject> inventorySlots = new List<GameObject>();
    private int itemCount = 0;
    [SerializeField] private GameObject InventoryObjectRotationPoint;

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
        // Instantiate the corresponding prefab in the inventory UI set the prefab using the index of the prefab and the itemCount number
        GameObject itemPrefab = itemPrefabs[(int)inventoryItem - 1];
        GameObject itemInstance = Instantiate(itemPrefab, inventorySlots[itemCount].transform);

        // instantiate an itemPrefab as a child of InventoryObjectRotationPoint
        GameObject itemInstanceRotation = Instantiate(itemPrefab, InventoryObjectRotationPoint.transform);

        
        itemInstance.transform.localPosition = Vector3.zero;
        itemCount++;
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
