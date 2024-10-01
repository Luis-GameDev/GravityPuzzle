using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Bewegungsgeschwindigkeit und Sprungkraft
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    // Bodenüberprüfung
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    // Referenzen
    private Rigidbody2D rb;
    public bool isGrounded;
    private bool facingRight = true;

    // Schwerkraft-Richtungen
    private Vector2 gravityDirection = Vector2.down;
    private Vector3 rotationAngle = Vector3.zero;

    void Start()
    {
        // Referenz zum Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        
        // Setze die standardmäßige Schwerkraft
        Physics2D.gravity = gravityDirection * 9.81f;
    }

    void Update()
    {
        // Bewegung links und rechts basierend auf der aktuellen Schwerkraft
        float moveInput = Input.GetAxis("Horizontal");
        
        // Bewege den Charakter in die Richtung der aktuellen Schwerkraft (seitlich)
        Vector2 moveVelocity = GetMovementDirection() * moveInput * moveSpeed;
        rb.velocity = new Vector2(moveVelocity.x, rb.velocity.y);  // Bewegung ohne vertikalen Einfluss

        // Überprüfung, ob der Charakter auf dem Boden ist
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Sprung nur, wenn auf dem Boden
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);  // Setze vertikale Geschwindigkeit zurück, bevor der Sprung erfolgt
            rb.AddForce(-gravityDirection * jumpForce, ForceMode2D.Impulse);  // Springe entgegen der aktuellen Schwerkraft
        }

        // Überprüfe und ändere die Gravitation mit den Pfeiltasten
        HandleGravityChange();
    }

    // Bewegung basierend auf der aktuellen Schwerkraft (links/rechts)
    Vector2 GetMovementDirection()
    {
        if (gravityDirection == Vector2.down || gravityDirection == Vector2.up)
        {
            return new Vector2(1, 0); // Bewegung entlang der X-Achse (links/rechts)
        }
        else
        {
            return new Vector2(0, 1); // Bewegung entlang der Y-Achse (oben/unten)
        }
    }

    // Handhabung der Schwerkraftänderung mit den Pfeiltasten
    void HandleGravityChange()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gravityDirection = Vector2.up;
            rotationAngle = new Vector3(0, 0, 180); // Drehe um 180 Grad
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gravityDirection = Vector2.down;
            rotationAngle = Vector3.zero; // Normale Ausrichtung
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gravityDirection = Vector2.left;
            rotationAngle = new Vector3(0, 0, 90); // Drehe um 90 Grad im Uhrzeigersinn
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gravityDirection = Vector2.right;
            rotationAngle = new Vector3(0, 0, -90); // Drehe um 90 Grad gegen den Uhrzeigersinn
        }

        // Setze die neue Schwerkraft
        Physics2D.gravity = gravityDirection * 9.81f;

        // Drehe den Charakter entsprechend der neuen Schwerkraftrichtung
        transform.eulerAngles = rotationAngle;
    }
}
