using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveDirection = Vector2.zero;
    private GravityController gravityController;

    // Animation-related fields
    public Sprite standingRight;
    public Sprite walkingRight;
    public Sprite standingLeft;
    public Sprite walkingLeft;
    private SpriteRenderer spriteRenderer;

    private bool facingRight = true; 
    private float animationTimer = 0f;
    public float animationSpeed = 0.1f; 

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        rb = GetComponent<Rigidbody2D>();
        gravityController = GetComponent<GravityController>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
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
        facingRight = false; // Facing left
        AnimateWalking();
    }

    private void OnMoveRight(InputAction.CallbackContext context)
    {
        moveDirection = GetRelativeDirection(1f);
        facingRight = true; // Facing right
        AnimateWalking();
    }

    private void OnStopMove(InputAction.CallbackContext context)
    {
        moveDirection = Vector2.zero;
        SetStandingSprite();
    }

    private void FixedUpdate()
    {
        if (gravityController.isGrounded)
        {
            rb.velocity = moveDirection * moveSpeed;
        }

        // Update the walking animation when moving
        if (moveDirection != Vector2.zero)
        {
            AnimateWalking();
        }
    }

    // Determine relative movement based on gravity direction
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

    // Animate walking by cycling between standing and walking sprites
    private void AnimateWalking()
    {
        animationTimer += Time.deltaTime;

        if (animationTimer >= animationSpeed)
        {
            if (facingRight)
            {
                // Toggle between walkingRight and standingRight
                if (spriteRenderer.sprite == walkingRight)
                {
                    spriteRenderer.sprite = standingRight;
                }
                else
                {
                    spriteRenderer.sprite = walkingRight;
                }
            }
            else
            {
                // Toggle between walkingLeft and standingLeft
                if (spriteRenderer.sprite == walkingLeft)
                {
                    spriteRenderer.sprite = standingLeft;
                }
                else
                {
                    spriteRenderer.sprite = walkingLeft;
                }
            }

            // Reset the animation timer
            animationTimer = 0f;
        }
    }

    // Set the standing sprite based on the last direction faced
    private void SetStandingSprite()
    {
        if (facingRight)
        {
            spriteRenderer.sprite = standingRight;
        }
        else
        {
            spriteRenderer.sprite = standingLeft;
        }
    }
}
