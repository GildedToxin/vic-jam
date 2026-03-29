using UnityEngine;

public class EndGame : Interactable
{
    private void Update()
    {
        if (FindAnyObjectByType<InvisableMaze>().GetCanCompleteRoom())
        {
            PromptText = "Press E to leave the room";
        }
        else if (!FindAnyObjectByType<InvisableMaze>().GetCanCompleteRoom())
        {
            PromptText = "";
        }
    }
    public override void Interact(PlayerController player)
    {
        if(FindAnyObjectByType<InvisableMaze>().GetCanCompleteRoom())
            print("Game End");
    }
    
}
