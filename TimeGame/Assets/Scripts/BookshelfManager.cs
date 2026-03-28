using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class BookshelfManager : Lock
{
    public GameObject missingBook;
    public GameObject highlightBook;
    public GameObject gear;

    public GameObject[] books;

    public GameObject[] bookSolution;

    public Vector2 input;

    public int bookIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject firstbook;

    bool canUseBookself = true;

    public override void Interact(PlayerController player)
    {
        if(canUseBookself == false) return;

        if (missingBook.activeSelf)
            EnterBookshelf();
        else
        {
            base.Interact(player);
        }
    }
    public void UpdateInput(Vector2 input)
    {
        this.input = input.normalized;
        input = input.normalized;
        if(input.x > 0.5f)
        {
            bookIndex++;
            if (bookIndex >= books.Length) bookIndex = 0;
            highlightBook.transform.position = books[bookIndex].transform.position;
        }
        else if(input.x < -0.5f)
        {
            bookIndex--;
            if (bookIndex < 0) bookIndex = books.Length - 1;
            highlightBook.transform.position = books[bookIndex].transform.position;
        }
    }
    public void SelectBook()
    {
        if(firstbook != null) 
        {
            SwapBooks();
            UserInterfaceManager.Instance.SetText("Press E to select a book to swap");
            return;
        }
        firstbook = books[bookIndex];
        UserInterfaceManager.Instance.SetText("Press E to swap book");
    }
    public bool CheckOrder()
    {
        int i = 0;
        foreach(var book in books)
        {
            if (book != bookSolution[i])
                return false;
            i++;
        }
        return true;
    }
    public void SwapBooks()
    {
        var temp = firstbook.transform.position;
        firstbook.transform.position = books[bookIndex].transform.position;
        books[bookIndex].transform.position = temp;


        int firstIndex = Array.IndexOf(books, firstbook);

        // Swap the positions in the array as well to keep track of the new order
        books[firstIndex] = books[bookIndex];
        books[bookIndex] = firstbook;

        firstbook = null;

        if (CheckOrder())
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            canUseBookself = false;
            LeaveBookshelf();
            StartCoroutine(ShowGear());
        }
    }
    public override void UseItem()
    {

        missingBook.SetActive(true);
        FindAnyObjectByType<PlayerController>().isInBookshelf = true;
        Camera.main.GetComponent<CameraSwitcher>().SwitchToInteractCam();
        //UserInterfaceManager.Instance.HideInteract();
        UserInterfaceManager.Instance.SetText("Press E to select a book to swap");
        UserInterfaceManager.Instance.ShowHideBookshelfText();
    }

    public void LeaveBookshelf()
    {
        FindAnyObjectByType<PlayerController>().isInBookshelf = false;
        Camera.main.GetComponent<CameraSwitcher>().SwitchToFreeLook();
        highlightBook.SetActive(false);
        UserInterfaceManager.Instance.SetText("Press E to interact");
        UserInterfaceManager.Instance.ShowHideBookshelfText();
        firstbook = null;
    }
    public void EnterBookshelf()
    {
        FindAnyObjectByType<PlayerController>().isInBookshelf = true;
        Camera.main.GetComponent<CameraSwitcher>().SwitchToInteractCam();
        highlightBook.SetActive(true);
        UserInterfaceManager.Instance.SetText("Press E to select a book to swap");
        UserInterfaceManager.Instance.ShowHideBookshelfText();
    }
    [ContextMenu ("Show Gear")]
    public IEnumerator ShowGear()
    {
        yield return new WaitForSeconds(0.5f);
        gear.SetActive(true);
    }
}
