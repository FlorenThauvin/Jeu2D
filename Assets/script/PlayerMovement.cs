using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public bool isJumping;
    public bool isGrounded;
    public Transform GroundCheckLeft;
    public Transform GroundCheckRight;
    public Rigidbody2D rb;
    public Animator animator;
    private Vector3 velocity = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(GroundCheckLeft.position, GroundCheckRight.position);

        // Quand on retouche le sol → fin du saut
        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            animator.SetBool("IsJumping", false);
        }

        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;

        if (Input.GetButton("Jump") && isGrounded)
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
        }

        MovePlayer(horizontalMovement);
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.linearVelocity.y);
        rb.linearVelocity = Vector3.SmoothDamp(rb.linearVelocity, targetVelocity, ref velocity, .05f);

        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

}
