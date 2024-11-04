using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement variables
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public bool _isGrounded;

    // Reference to the Rigidbody
    public Rigidbody2D rb;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;


    void Start()
    {
        // Initialize the Rigidbody component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input for horizontal movement (left/right)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Move the player horizontally
        Vector2 move = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = move;

        _isGrounded = IsGrounded();

        // Jump if the player is on the ground and the jump key is pressed
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public bool IsGrounded()
    {
        // Perform BoxCast to detect if the player is on the ground
        return Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.down, castDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
    }
}
