using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    
public class GravityController : MonoBehaviour
{
    private Rigidbody2D rb;
    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public delegate void OnDeathTriggered();
    //[HideInInspector] public static event OnDeathTriggered OnDeath;
    [HideInInspector] public Direction _playerDirection = Direction.Down;
    private PlayerInputActions playerControls;
    private InputAction SwitchUp;
    private InputAction SwitchDown;
    private InputAction SwitchLeft;
    private InputAction SwitchRight;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void Awake()
    {
        //groundLayer = LayerMask.NameToLayer("Ground");
        playerControls = new PlayerInputActions();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerDirection = Direction.Down;
    }

    private void OnEnable()
    {
        SwitchUp = playerControls.GravitySwitch.SwitchUp;
        SwitchUp.Enable();
        SwitchUp.performed += SwitchGravityUp;

        SwitchDown = playerControls.GravitySwitch.SwitchDown;
        SwitchDown.Enable();
        SwitchDown.performed += SwitchGravityDown;

        SwitchLeft = playerControls.GravitySwitch.SwitchLeft;
        SwitchLeft.Enable();
        SwitchLeft.performed += SwitchGravityLeft;

        SwitchRight = playerControls.GravitySwitch.SwitchRight;
        SwitchRight.Enable();
        SwitchRight.performed += SwitchGravityRight;
    }

    private void OnDisable()
    {
        SwitchUp.Disable();
        SwitchDown.Disable();
        SwitchLeft.Disable();
        SwitchRight.Disable();
    }

    void TriggerDeath()
    {
        //OnDeath();
        _playerDirection = Direction.Down;
        Physics2D.gravity = new Vector2(0f, -9.81f);
        Rotation(0);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Deadly"))
        {
            TriggerDeath();
        }
    }

    private void SwitchGravityUp(InputAction.CallbackContext context)
    {
        if(isGrounded)
        {
            _playerDirection = Direction.Up;
            Physics2D.gravity = new Vector2(0f, 9.81f);  
            Rotation(180);
            ResetPlayerPhysics();
        }
    }

    private void SwitchGravityDown(InputAction.CallbackContext context)
    {
        if(isGrounded)
        {
            _playerDirection = Direction.Down;
            Physics2D.gravity = new Vector2(0f, -9.81f);
            Rotation(0);
            ResetPlayerPhysics();
        }
    }

    private void SwitchGravityLeft(InputAction.CallbackContext context)
    {
        if(isGrounded)
        {
            _playerDirection = Direction.Left;
            Physics2D.gravity = new Vector2(-9.81f, 0f);
            Rotation(-90);
            ResetPlayerPhysics();
        }
    }

    private void SwitchGravityRight(InputAction.CallbackContext context)
    {
        if(isGrounded)
        {
            _playerDirection = Direction.Right;
            Physics2D.gravity = new Vector2(9.81f, 0f);
            Rotation(90);
            ResetPlayerPhysics();
        }  
    }

    public void Rotation(float rotation)
    {
        transform.rotation = Quaternion.Euler(0,0,rotation);
    }

    private void ResetPlayerPhysics()
    {
        Rigidbody2D player = gameObject.GetComponent<Rigidbody2D>();

        if(player != null)
        {
            player.velocity = Vector2.zero;            
            player.angularVelocity = 0f; 
            player.Sleep(); 
        }
    }
}
