using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed;

    public LayerMask groundLayer;

    public Rigidbody2D rb;
    public Animator enemyAnim;

    public Transform raycastOrigin;

    [SerializeField] private bool _isGrounded;
    private bool _facingRight = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = groundCheck();
        
        if(!_isGrounded)
        {
            Flip();
        }

        Move();
    }

    bool groundCheck()
    {
        Vector2 position = raycastOrigin.position;
        Vector2 direction = Vector2.down;

        float distance = 2f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        Debug.DrawRay(position, direction, Color.red);

        return hit.collider != null;
    }

    private void Move()
    {
        float moveDir = _facingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(moveDir * speed, rb.linearVelocity.y);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;

        // Flip the enemy's local scale to face the other direction
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
