using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f; // Jump force
    private Vector2 moveDirection = Vector2.zero;
    private GravityController gravityController;
    public Sprite standingRight;
    public Sprite walkingRight;
    public Sprite standingLeft;
    public Sprite walkingLeft;
    private SpriteRenderer spriteRenderer;
    private bool isWalkingLeft = false;
    private bool isWalkingRight = false;
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

        // Register move left/right actions
        playerInputActions.Movement.MoveLeft.performed += OnMoveLeft;
        playerInputActions.Movement.MoveLeft.canceled += OnStopMoveLeft;

        playerInputActions.Movement.MoveRight.performed += OnMoveRight;
        playerInputActions.Movement.MoveRight.canceled += OnStopMoveRight;

        // Register jump action
        playerInputActions.Movement.Jump.performed += OnJump;
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
        isWalkingLeft = true;
    }

    private void OnMoveRight(InputAction.CallbackContext context)
    {
        moveDirection = GetRelativeDirection(1f);
        facingRight = true; // Facing right
        AnimateWalking();
        isWalkingRight = true;
    }

    private void OnStopMoveLeft(InputAction.CallbackContext context)
    {
        isWalkingLeft = false;

        if(!isWalkingRight)
        {
            moveDirection = Vector2.zero;
            SetStandingSprite();   
        }
        else
        {
            moveDirection = GetRelativeDirection(1f);
            facingRight = true; // Facing right
            AnimateWalking();
            isWalkingRight = true;
        }
    }

    private void OnStopMoveRight(InputAction.CallbackContext context)
    {
        isWalkingRight = false;

        if(!isWalkingLeft)
        {
            moveDirection = Vector2.zero;
            SetStandingSprite();
        }
        else
        {
            moveDirection = GetRelativeDirection(-1f);
            facingRight = false; // Facing left
            AnimateWalking();
            isWalkingLeft = true;
        }
    }

    // Handle the Jump action
    private void OnJump(InputAction.CallbackContext context)
    {
        // Only jump if grounded
        if (gravityController.isGrounded)
        {
            Vector2 jumpDirection = GetJumpDirection();
            rb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Calculate the jump direction based on the current gravity
    private Vector2 GetJumpDirection()
    {
        Direction playerDirection = gravityController._playerDirection;

        switch (playerDirection)
        {
            case Direction.Up:
                return Vector2.down;  // Jump down when gravity is upwards
            case Direction.Down:
                return Vector2.up;    // Jump up when gravity is downwards (normal gravity)
            case Direction.Right:
                return Vector2.left;  // Jump left when gravity is to the right
            case Direction.Left:
                return Vector2.right; // Jump right when gravity is to the left
            default:
                return Vector2.up;    // Default to upwards if no gravity direction is found
        }
    }

    private void FixedUpdate()
    {
        if (gravityController.isGrounded)
        {
            // Apply velocity based on the current gravity state
            Direction playerDirection = gravityController._playerDirection;
            if (playerDirection == Direction.Down || playerDirection == Direction.Up)
            {
                // Horizontal movement in Up/Down gravity
                rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
            }
            else
            {
                // Vertical movement in Left/Right gravity
                rb.velocity = new Vector2(rb.velocity.x, moveDirection.y * moveSpeed);
            }
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
