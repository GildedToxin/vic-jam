using UnityEngine;

public class PickUpItem : Interactable
{
    [SerializeField] private InventoryItem InventoryItem = InventoryItem.None;

    public BoxCollider boxCollider;
    public override void Interact(PlayerController player)
    {
        base.Interact(player);
        Debug.Log("Picked up " + gameObject.name);
        player.playerInventory.AddItem(InventoryItem);

        Destroy(gameObject);
        UserInterfaceManager.Instance.HideInteract();
    }
}

