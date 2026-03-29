using UnityEngine;

public class InventoryButtonScript : MonoBehaviour
{
    public string parentName = null;
    private GameObject inventoryUIScript;

    void Start()
    {
        inventoryUIScript = GameObject.Find("InventoryCanvas");
    }

    public void OnInventoryButtonPressed()
    {
        parentName = transform.parent.name;
        Debug.Log("Inventory button pressed on " + parentName);

        inventoryUIScript.GetComponent<InventoryItemRotation>().buttonParentName = parentName;
    }
}
