using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string promptText = "Press E to interact";
    public virtual void Interact(PlayerController player)
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
}