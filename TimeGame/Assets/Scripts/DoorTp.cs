using UnityEngine;

public class DoorTp : Interactable
{
    public Transform targetLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Interact(PlayerController player)
    {
        base.Interact(player);
        FindAnyObjectByType<PlayerController>().transform.position = targetLocation.transform.position;
        print("e");
    }
}
