using UnityEngine;

public class DoorTp : Interactable
{
    public Transform targetLocation;
    public bool mainDoor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Interact(PlayerController player)
    {
        base.Interact(player);
        player.GetComponent<CharacterController>().enabled = false; // Disable the CharacterController to prevent physics issues during teleportation
        player.transform.position = targetLocation.transform.position;
        player.GetComponent<CharacterController>().enabled = true; // Re-enable the CharacterController after teleportation
        print("e");

        if (mainDoor)
        {
            Camera.main.GetComponent<CameraSwitcher>().SwitchToSideRoom();
        }
        else
            Camera.main.GetComponent<CameraSwitcher>().SwitchToFreeLook();
    }
}
