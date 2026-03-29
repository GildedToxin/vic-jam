using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueScript : MonoBehaviour
{
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;

    public Dialogue currentDialogue;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwapDialogue()
    {
        // switch case to set dialogueText based on currentDialogue
        switch (currentDialogue)
        {
            case Dialogue.GraveFake:
                dialogueText.text = "A grave, the writing is worn down, best leave it be.";
                break;
            case Dialogue.GraveReal:
                dialogueText.text = "The Writing here is legible: 'RIP GERALD DIED AS HE LIVED, TOD: 5:50";
                break;
            case Dialogue.Bookshelf:
                dialogueText.text = "It's a bookshelf, the books are really dusty.";
                break;
            case Dialogue.GearBox:
                dialogueText.text = "It's a gear box, but it's missing a component.";
                break;
            case Dialogue.Clock:
                dialogueText.text = "A massive clock remains still, somethings off about it.";
                break;
            case Dialogue.Statue:
                dialogueText.text = "A statue depicting the ferryman leading souls into the after life, there is text written on the pedestal: 'Safe Guidance to those who pay the toll'";
                break;
            case Dialogue.Gargoyle:
                dialogueText.text = "A gargoyle wielding a sword, its pointing in a direction.";
                break;
            case Dialogue.PickupItem:
                dialogueText.text = "You picked up an item.";
                break;
        }
    }

    public void DisplayDialogue(Dialogue dialogue)
    {
        dialogueUI.SetActive(true);
        currentDialogue = dialogue;
        SwapDialogue();
    }

    public IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialogueUI.SetActive(false);
    }
}
public enum Dialogue
    {
        GraveFake,
        GraveReal,
        Bookshelf,
        GearBox,
        Clock,
        Statue,
        Gargoyle,
        PickupItem,
    }
