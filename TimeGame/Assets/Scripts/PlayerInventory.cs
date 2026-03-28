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
        
        GameObject itemPrefab = itemPrefabs[(int)inventoryItem - 1];
        GameObject itemInstance = Instantiate(itemPrefab, inventorySlots[itemCount].transform);
        itemInstance.transform.localScale = itemPrefab.transform.localScale * 0.5f;
        itemInstance.GetComponent<RectTransform>().localPosition -= new Vector3(0, 0, 30);

        //GameObject itemInstanceRotation = Instantiate(itemPrefab, InventoryObjectRotationPoint.transform);
        itemCount++;
    }
    public void RemoveItem(InventoryItem inventoryItem)
    {
        if (items.Contains(inventoryItem))
        {
            items.Remove(inventoryItem);

            GameObject itemPrefab = itemPrefabs[(int)inventoryItem - 1];
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if (inventorySlots[i].transform.childCount > 0)
                {
                    GameObject child = inventorySlots[i].transform.GetChild(0).gameObject;
                    if (child.name.Contains(itemPrefab.name))
                    {
                        Destroy(child);
                        itemCount--;
                        break;
                    }
                }
            }
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
