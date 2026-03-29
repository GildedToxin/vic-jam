using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Jumping & Gravity")]
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("References")]
    public Transform cameraTransform;
    public PlayerInteraction playerInteraction;
    public PlayerInventory playerInventory;
    public CinemachineCamera freeCamera;
    private CinemachineInputAxisController cinemachineInput;

    private CharacterController controller;
    private Vector3 velocity;

    [SerializeField] private Vector2 moveInput;
    private bool jumpPressed;

    [Header("State")]
    private bool isPushing = false;
    public bool isInBookshelf = false;
    public bool isInClock = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        cinemachineInput = freeCamera.GetComponent<CinemachineInputAxisController>();
    }

    void Update()
    {
        if (isInBookshelf || isInClock)
            return;
        if (!isPushing)
        {
            HandleMovement();
            HandleGravity();
        }


        cinemachineInput.enabled = !playerInventory.isInventoryOpen;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isInBookshelf || isInClock)
        {
            moveInput = Vector2.zero; // Prevent movement while in bookshelf
        }
        if (isInBookshelf && context.started)
        {
            FindAnyObjectByType<BookshelfManager>().UpdateInput(context.ReadValue<Vector2>());
        }
        else if (isInClock && context.started)
        {
            FindAnyObjectByType<ClockManager>().UpdateInput(context.ReadValue<Vector2>());
        }
        else if (!isInBookshelf && !isInClock)
            moveInput = context.ReadValue<Vector2>();
    }
    public void OnEscape(InputAction.CallbackContext context)
    {
        if (isInBookshelf && context.started)
        {
            FindAnyObjectByType<BookshelfManager>().LeaveBookshelf();
        }
        else if (isInBookshelf && context.started)
        {
            FindAnyObjectByType<ClockManager>().LeaveClock();
        }
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            jumpPressed = true;
    }

    void HandleMovement()
    {
        bool isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        Vector3 inputDir = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        if (inputDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }

        if (jumpPressed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpPressed = false;
        }
    }

    void HandleGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void SetIsPushing(bool pushing)
    {
     isPushing = pushing;
    }
}