using UnityEngine;

public class EndGame : Interactable
{
    public override void Interact(PlayerController player)
    {
        if(FindAnyObjectByType<InvisableMaze>().GetCanCompleteRoom())
            print("Game End");
    }
    
}
