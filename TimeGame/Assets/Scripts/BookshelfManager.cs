using System;
using System.Collections;
using UnityEngine;


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

    public AudioSource source;

    public AudioClip puzzel;

    [SerializeField] private float fallDistance = 2f;
    [SerializeField] private float fallDuration = 0.5f;
    [SerializeField] private AnimationCurve fallCurve; // ease-in/out

    private bool isFalling = false;
    public override void Interact(PlayerController player)
    {
        if(canUseBookself == false) 
        {
            return;
        }

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
        PlaySound();
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
            AudioPool.Instance.PlayClip2D(puzzel, 1f);
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
        highlightBook.SetActive(true);
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
    public void ShowTheGear()
    {
        StartCoroutine(ShowGear());
    }
    public IEnumerator ShowGear()
    {
        gear.SetActive(true);
        isFalling = true;

        Vector3 startPos = gear.transform.position;
        Vector3 endPos = startPos + Vector3.back * fallDistance;

        float time = 0f;

        while (time < fallDuration)
        {
            float t = time / fallDuration;

            // Apply easing
            float curveT = fallCurve != null ? fallCurve.Evaluate(t) : t;

            gear.transform.position = Vector3.Lerp(startPos, endPos, curveT);

            gear.transform.Rotate(new Vector3(0, 0, 90f) * Time.deltaTime);


            time += Time.deltaTime;
            yield return null;
        }

        gear.transform.position = endPos;

        isFalling = false;
        gear.GetComponent<BoxCollider>().isTrigger = false; // Enable collider after falling
    }

    private void PlaySound()
    {
        if (source != null)
        {
            source.Play();
        }
    }
}
