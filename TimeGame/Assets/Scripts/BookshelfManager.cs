using System;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class BookshelfManager : Lock
{
    public GameObject missingBook;
    public GameObject highlightBook;

    public GameObject[] books;

    public Vector2 input;

    public int bookIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject firstbook;

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
        print("e");
        if(firstbook != null) 
        {
            SwapBooks();
            print("e2");
            return;
        }
        firstbook = books[bookIndex];
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
    }
    public override void UseItem()
    {

        missingBook.SetActive(true);
        FindAnyObjectByType<PlayerController>().isInBookshelf = true;
        Camera.main.GetComponent<CameraSwitcher>().SwitchToInteractCam();
    }
}
