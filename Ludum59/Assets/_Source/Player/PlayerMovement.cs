using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    
    [Header("Ground check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundCheckMask;

    private Rigidbody2D _rb;
    private bool _isGrounded;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(
             groundCheck.position,
             groundCheckRadius,
             groundCheckMask);
    }

    public void Move(Vector2 movement)
    {
        Vector2 velocity = movement * speed;
        velocity.y = _rb.linearVelocity.y;
        _rb.linearVelocity = velocity;
    }

    public void Jump()
    {
        Debug.Log("Jump");
        if (_isGrounded)
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        else
            Debug.Log("Something went wrong");
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }
#endif
}