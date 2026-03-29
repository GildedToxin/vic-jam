using NUnit.Framework;
using UnityEngine;

public class Lock : Interactable
{
   [SerializeField] InventoryItem requiredItem = InventoryItem.None;

    public override void Interact(PlayerController player)
    {
        base.Interact(player);

        if(DoesPlayerHaveItem(player))
        {
            UseItem();
            RemoveItem(player);
           // Debug.Log("Player has the required item. Unlocking...");
            // Implement unlocking logic here (e.g., open a door, disable a barrier, etc.)
        }
        else
        {
            player.dialogueScript.currentDialogue = Dialogue.Bookshelf;
            player.dialogueScript.DisplayDialogue();
            StartCoroutine(player.dialogueScript.HideDialogueAfterDelay(2f));
            Debug.Log("Player does not have the required item. Cannot unlock.");
        }
    }

    public bool DoesPlayerHaveItem(PlayerController player)
    {
        if(player.playerInventory.DoesPlayerHaveItem(requiredItem))
            return true;

        return false;
    }

    public void RemoveItem(PlayerController player)
    {
        player.playerInventory.RemoveItem(requiredItem);
    }
    public virtual void UseItem()
    {
        // Implement any visual or audio feedback for using the item here (e.g., play a sound, show an animation, etc.)
    }
}
