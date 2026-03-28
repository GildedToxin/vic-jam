using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactLayer;
    public Transform cameraTransform;
    [SerializeField] private PlayerController playerController;

    [SerializeField] private Interactable currentInteractable;


    void Update()
    {
        CheckForInteractable();
    }

    void CheckForInteractable()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                if (currentInteractable != interactable)
                {
                    currentInteractable = interactable;
                   // Debug.Log("Looking at: " + interactable.name);
                    UserInterfaceManager.Instance.ShowInteract(interactable);    
                }
                return;
            }
        }

        if (currentInteractable != null)
        {
            currentInteractable = null;
            UserInterfaceManager.Instance.HideInteract();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //if (!context.performed) return;

        if (playerController.isInBookshelf && context.started)
        {
            FindAnyObjectByType<BookshelfManager>().SelectBook();
        }
        else if (!playerController.isInBookshelf && currentInteractable != null)
        {
            currentInteractable.Interact(playerController);
        }
    }
}