using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact(PlayerController player)
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
}