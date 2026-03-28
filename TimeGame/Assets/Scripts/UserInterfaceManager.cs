using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class UserInterfaceManager : MonoBehaviour
{
    public static UserInterfaceManager Instance;

    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI bookshelfText;



    private void Awake()
    {
        Instance = this;
    }
    public void Show()
    {
        interactPrompt.SetActive(true);
    }

    public void ShowInteract(Interactable interactable)
    {
        interactPrompt.SetActive(true);
        interactText.text = interactable.PromptText; // or custom per object
    }
    public void SetText(string text)
    {
        interactText.text = text;
    }
    public void HideInteract()
    {
        interactPrompt.SetActive(false);
    }

    public void ShowHideBookshelfText()
    {
        bookshelfText.gameObject.SetActive(!bookshelfText.gameObject.activeSelf);
    }
}
