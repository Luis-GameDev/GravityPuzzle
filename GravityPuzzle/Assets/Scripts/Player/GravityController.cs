using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GravityController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool top;
    private bool isGrounded = true;
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    public Direction _playerDirection;
    public PlayerInputActions playerControls;
    private InputAction SwitchUp;
    private InputAction SwitchDown;
    private InputAction SwitchLeft;
    private InputAction SwitchRight;



    void Awake()
    {
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

    private void SwitchGravityUp(InputAction.CallbackContext context)
    {
        _playerDirection = Direction.Up;
        Physics2D.gravity = new Vector2(0f, 9.81f);  
        Rotation(180);
    }

    private void SwitchGravityDown(InputAction.CallbackContext context)
    {
        _playerDirection = Direction.Down;
        Physics2D.gravity = new Vector2(0f, -9.81f);
        Rotation(0);
    }

    private void SwitchGravityLeft(InputAction.CallbackContext context)
    {
        _playerDirection = Direction.Left;
        Physics2D.gravity = new Vector2(-9.81f, 0f);
        Rotation(-90);
    }

    private void SwitchGravityRight(InputAction.CallbackContext context)
    {
        _playerDirection = Direction.Right;
        Physics2D.gravity = new Vector2(9.81f, 0f);
        Rotation(90);
    }

    void Rotation(float rotation)
    {
        transform.rotation = Quaternion.Euler(0,0,rotation);
    }
}
