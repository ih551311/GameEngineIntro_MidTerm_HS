using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator myAnimator;
    public SpriteRenderer sr;

    public float moveSpeed = 5.0f;
    public float jumpForce = 8.0f;
    public float baseMoveSpeed;
    public float baseJumpForce;
    public bool isInvincible;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();  
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        baseMoveSpeed = moveSpeed;
        baseJumpForce = jumpForce;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveToNextLevel();
        }
        if (collision.CompareTag("Respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.CompareTag("Enemy")&&!isInvincible)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Invincibility(float duration)
    {
        if (!isInvincible)
            StartCoroutine(InvincibilityRoutine(duration));
    }

    private IEnumerator InvincibilityRoutine(float duration)
    {
        isInvincible = true;
        Debug.Log("무적 시작!");

        // 무적 중 깜빡임 효과 (선택)
        float timer = 0f;
        while (timer < duration)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }
        sr.enabled = true;

        isInvincible = false;
        Debug.Log("무적 종료");
    }

    public void SpeedBoost(float multiplier, float duration)
    {
        StartCoroutine(SpeedBoostRoutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {
        moveSpeed = baseMoveSpeed * multiplier;
        Debug.Log("이동속도 증가!");

        yield return new WaitForSeconds(duration);

        moveSpeed = baseMoveSpeed;
        Debug.Log("이동속도 원상복구");
    }

    public void JumpBoost(float multiplier, float duration)
    {
        StartCoroutine(JumpBoostRoutine(multiplier, duration));
    }

    private IEnumerator JumpBoostRoutine(float multiplier, float duration)
    {
        jumpForce = baseJumpForce * multiplier;
        Debug.Log("점프력 증가!");

        yield return new WaitForSeconds(duration);

        jumpForce = baseJumpForce;
        Debug.Log("점프력 원상복구");
    }
    private void Update()
    {
        if(moveInput > 0)
        {
            transform.localScale = new Vector3(0.13f, 0.13f, 0.13f);
        }
        else if(moveInput < 0)
        {
            transform.localScale = new Vector3(-0.13f, 0.13f, 0.13f);
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
}

