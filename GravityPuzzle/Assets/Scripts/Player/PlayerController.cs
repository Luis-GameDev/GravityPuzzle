using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveDirection = Vector2.zero;
    private GravityController gravityController;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();

        gravityController = GetComponent<GravityController>();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();

        playerInputActions.Movement.MoveLeft.performed += OnMoveLeft;
        playerInputActions.Movement.MoveLeft.canceled += OnStopMove;
        
        playerInputActions.Movement.MoveRight.performed += OnMoveRight;
        playerInputActions.Movement.MoveRight.canceled += OnStopMove;
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void OnMoveLeft(InputAction.CallbackContext context)
    {
        moveDirection = GetRelativeDirection(-1f);
    }

    private void OnMoveRight(InputAction.CallbackContext context)
    {
        moveDirection = GetRelativeDirection(1f);
    }

    private void OnStopMove(InputAction.CallbackContext context)
    {
        moveDirection = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if(gravityController.isGrounded)
        {
            rb.velocity = moveDirection * moveSpeed;
        }
    }

    private Vector2 GetRelativeDirection(float horizontalInput)
    {
        Direction playerDirection = gravityController._playerDirection;

        switch (playerDirection)
        {
            case Direction.Up:
                return new Vector2(-horizontalInput, 0f); // Left becomes right relative to Up
            case Direction.Down:
                return new Vector2(horizontalInput, 0f);  // Normal left/right for Down gravity
            case Direction.Right:
                return new Vector2(0f, horizontalInput);  // Left/right becomes up/down relative to Right gravity
            case Direction.Left:
                return new Vector2(0f, -horizontalInput); // Left/right becomes down/up relative to Left gravity
            default:
                return Vector2.zero;
        }
    }
}
