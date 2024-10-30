using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f; 
    [SerializeField] private float jumpTime = 0.4f;
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
    private bool isJumping = false;
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
        playerInputActions.Movement.MoveLeft.canceled += OnStopMoveLeft;

        playerInputActions.Movement.MoveRight.performed += OnMoveRight;
        playerInputActions.Movement.MoveRight.canceled += OnStopMoveRight;

        playerInputActions.Movement.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }

    private void OnMoveLeft(InputAction.CallbackContext context)
    {
        moveDirection = GetRelativeDirection(-1f);
        facingRight = false; 
        AnimateWalking();
        isWalkingLeft = true;
    }

    private void OnMoveRight(InputAction.CallbackContext context)
    {
        moveDirection = GetRelativeDirection(1f);
        facingRight = true; 
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
            facingRight = true; 
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
            facingRight = false; 
            AnimateWalking();
            isWalkingLeft = true;
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (gravityController.isGrounded)
        {
            Vector2 jumpDirection = GetJumpDirection();
            rb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
            StartCoroutine(LeaveJump(jumpTime));
        }
    }

    private IEnumerator LeaveJump(float delay)
    {
        yield return new WaitForSeconds(delay);

        isJumping = false;
    }

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
        print(isJumping);
        if (gravityController.isGrounded || isJumping)
        {
            Direction playerDirection = gravityController._playerDirection;
            if (playerDirection == Direction.Down || playerDirection == Direction.Up)
            {
                rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, moveDirection.y * moveSpeed);
            }
        }

        if (moveDirection != Vector2.zero)
        {
            AnimateWalking();
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

    private void AnimateWalking()
    {
        animationTimer += Time.deltaTime;

        if (animationTimer >= animationSpeed)
        {
            if (facingRight)
            {
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
                if (spriteRenderer.sprite == walkingLeft)
                {
                    spriteRenderer.sprite = standingLeft;
                }
                else
                {
                    spriteRenderer.sprite = walkingLeft;
                }
            }

            animationTimer = 0f;
        }
    }

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
