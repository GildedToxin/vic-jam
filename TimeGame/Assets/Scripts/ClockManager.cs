using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Assemblies;

public class ClockManager : Lock
{
    public GameObject hourHand;
    public GameObject minuteHand;

    public GameObject currentHand;

    public bool canUseClockself = true;

    public bool gearInPlace = false;

    public Vector2 input;

    public bool mouthSolved = false;
    public bool graveSovled = false;
    public AudioClip puzzel;


    private void Start()
    {
        currentHand = hourHand;
        PromptText = "";
    }
    public void Update()
    {
        if (FindAnyObjectByType<PlayerController>().playerInventory.items.Contains(InventoryItem.Gear))
        {
            PromptText = "Press E to adjust the clock";
        }
    }
    public override void Interact(PlayerController player)
    {
        if (canUseClockself == false) return;

        if (gearInPlace)
            EnterClock();
        else
        {
            base.Interact(player);
        }
    }
    public void SwapHands()
    {
        if(currentHand == hourHand)
        {
            currentHand = minuteHand;
        }
        else
        {
            currentHand = hourHand;
        }
    }

    [ContextMenu("Rotate Hand")]
    public void RotateHand(GameObject hand, float dir)
    {
        // I need to rotate it like a clock
        // 360 /12 = 30 degrees per hour
        if (hand == null) return;
        var degreestoRotate = 30f * dir; // Rotate 30 degrees per hour, multiplied by direction
        hand.transform.rotation = Quaternion.Euler(0, 0, hand.transform.rotation.eulerAngles.z + degreestoRotate);

        float minuteZ = minuteHand.transform.eulerAngles.z;
        float hourZ = hourHand.transform.eulerAngles.z;

        bool mouthMinute = Mathf.Abs(Mathf.DeltaAngle(minuteZ, 300f)) < 2f;
        bool mouthHour = Mathf.Abs(Mathf.DeltaAngle(hourZ, 150f)) < 2f;

        if (mouthMinute && mouthHour && !graveSovled)
        {
            graveSovled = true;
            FindAnyObjectByType<GraveStone>().MoveGrave();

            AudioPool.Instance.PlayClip2D(puzzel, 1f);
        }

        bool graveMinute = Mathf.Abs(Mathf.DeltaAngle(minuteZ, 90f)) < 2f;
        bool graveHour = Mathf.Abs(Mathf.DeltaAngle(hourZ, 0f)) < 2f;

        if (graveMinute && graveHour && !mouthSolved)
        {
            mouthSolved = true;
            FindAnyObjectByType<Ferryman>().OpenJaw();

            AudioPool.Instance.PlayClip2D(puzzel, 1f);
        }

        if (graveSovled && mouthSolved)
        {
            canUseClockself = false;
            LeaveClock();
            PromptText = "";
        }
    }

    public void EnterClock() {
        Camera.main.GetComponent<CameraSwitcher>().SwitchToClock();

        FindAnyObjectByType<PlayerController>().isInClock = true;
        Camera.main.GetComponent<CameraSwitcher>().SwitchToClock();
     
        UserInterfaceManager.Instance.SetText("Press E to change hands");
        UserInterfaceManager.Instance.ShowHideBookshelfText();
    }

    public void LeaveClock()
    {
        Camera.main.GetComponent<CameraSwitcher>().SwitchToFreeLook();


        FindAnyObjectByType<PlayerController>().isInClock = false;
        Camera.main.GetComponent<CameraSwitcher>().SwitchToFreeLook();
 
        UserInterfaceManager.Instance.SetText("Press E to interact");
        UserInterfaceManager.Instance.ShowHideBookshelfText();
    }

    public override void UseItem()
    {
        FindAnyObjectByType<PlayerController>().isInClock = true;
        Camera.main.GetComponent<CameraSwitcher>().SwitchToClock();
        //UserInterfaceManager.Instance.HideInteract();
        UserInterfaceManager.Instance.SetText("Press E to change hands");
        UserInterfaceManager.Instance.ShowHideBookshelfText();
        gearInPlace = true;
    }

    public void UpdateInput(Vector2 input)
    {
        this.input = input.normalized;
        input = input.normalized;
       
        if(input.x > 0.5f)
        {
            RotateHand(currentHand, 1);
        }
        else if(input.x < -0.5f)
        {
            RotateHand(currentHand, -1);
        }
    }

}
