using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement variables
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public bool _isGrounded;

    [SerializeField] private bool _isPlayerMoving = false;

    // Reference to the Rigidbody
    public Rigidbody2D rb;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    //Reference to the player renderer and the animator
    public SpriteRenderer playerSprite;
    public Animator animator;


    void Start()
    {
        // Initialize the Rigidbody component
        rb = GetComponent<Rigidbody2D>();
        _isPlayerMoving = false;
    }

    void Update()
    {
        HandleMoving();
    }

    public void HandleMoving()
    {
        // Get input for horizontal movement (left/right)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Determine if the player is moving based on input
        _isPlayerMoving = Mathf.Abs(horizontalInput) > 0.1f; 

        if (_isPlayerMoving)
        {
            // Move the player horizontally
            Vector2 move = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
            rb.linearVelocity = move;

            playerSprite.flipX = rb.linearVelocity.x < 0f;

            // Set the walking animation to true
            animator.SetBool("IsWalking", true);
        }
        else
        {
            // Set the walking animation to false if the player is not moving
            animator.SetBool("IsWalking", false);
        }

        // Check if the player is grounded
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
