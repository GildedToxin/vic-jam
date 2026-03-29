using UnityEngine;

public class GraveStoneScript : Interactable
{
    public bool realGrave = false;

    public override void Interact(PlayerController player)
    {
        if(realGrave == false) 
        {
            player.dialogueScript.currentDialogue = Dialogue.GraveFake;
            player.dialogueScript.DisplayDialogue();
            StartCoroutine(player.dialogueScript.HideDialogueAfterDelay(2f));
            return;
        }
        else if(realGrave == true)
        {
            player.dialogueScript.currentDialogue = Dialogue.GraveReal;
            player.dialogueScript.DisplayDialogue();
            StartCoroutine(player.dialogueScript.HideDialogueAfterDelay(2f));
        }
        else
        {
            base.Interact(player);
        }
    }
}
