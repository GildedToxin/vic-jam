using UnityEngine;

public class PickUpItem : Interactable
{
    [SerializeField] private InventoryItem InventoryItem = InventoryItem.None;
   // public override string PromptText { get; set; } = "Press E to interact";

    public BoxCollider boxCollider;
    public bool delete = false;
    public bool canInteract = true;
    public override void Interact(PlayerController player)
    {
        if(canInteract == false) return;
        base.Interact(player);
        Debug.Log("Picked up " + gameObject.name);
        player.playerInventory.AddItem(InventoryItem);

        UserInterfaceManager.Instance.HideInteract();
        canInteract = false;
        PromptText = "";
        if (delete)
            Destroy(gameObject);
    }
}

