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
        if (FindAnyObjectByType<InvisableMaze>().GetCanCompleteRoom())
        {
            foreach (var door in FindObjectsByType<DoorScript>())
            {
                door.RotateDoor();
            }
            FindAnyObjectByType<FadeToBlack>().FadeOut(); // fade to black
             // quit the application after fading out
        }
    }
    [ContextMenu("test")]
    public void test()
    {
        FindAnyObjectByType<FadeToBlack>().FadeOut(); // fade to black
    }
  
    
}
