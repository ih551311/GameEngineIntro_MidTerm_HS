using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator myAnimator;

    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();  
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(moveInput > 0)
        {
            transform.localScale = new Vector3(0.18f, 0.18f, 0.18f);
        }
        else if(moveInput < 0)
        {
            transform.localScale = new Vector3(-0.18f, 0.18f, 0.18f);
        }
        else
        {
            myAnimator.SetBool("move", false);
        }

            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded)
        {
            myAnimator.SetBool("jump", false);
        }
        else
        {
            myAnimator.SetBool("jump", true);
        }
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveInput = input.x;
        myAnimator.SetBool("move", true);
        if(moveInput != 0)
        {
            myAnimator.SetBool("move", true);
        }
        else
        {
            myAnimator.SetBool("move", false);
        }
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
