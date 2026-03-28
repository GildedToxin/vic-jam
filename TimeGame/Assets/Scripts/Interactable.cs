using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual string PromptText { get; set; } = "Press E to interact";
    public virtual void Interact(PlayerController player)
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
}