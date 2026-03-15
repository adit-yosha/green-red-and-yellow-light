using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public Transform spawnPoint;
    public PhaseManager phaseManager;

    public float yellowTolerance = 0.5f;
    public float redTolerance = 0.3f;

    private Rigidbody2D rb;
    private bool isGrounded;

    private float yellowTimer = 0f;
    private float redTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGround();
        Move();
        Jump();
        CheckRedPhase();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // RED PHASE → cek kalau bergerak
        if (phaseManager.currentPhase == PhaseManager.Phase.Red && Mathf.Abs(moveInput) > 0.1f)
        {
            redTimer += Time.deltaTime;

            if (redTimer >= redTolerance)
            {
                Die();
            }
        }
    }

    void Jump()
    {
        // YELLOW PHASE → tidak boleh lama di tanah
        if (phaseManager.currentPhase == PhaseManager.Phase.Yellow)
        {
            if (isGrounded)
            {
                yellowTimer += Time.deltaTime;

                if (yellowTimer >= yellowTolerance)
                {
                    Die();
                    return;
                }
            }
            else
            {
                yellowTimer = 0f;
            }
        }
        else
        {
            yellowTimer = 0f;
        }

        // RED PHASE → tidak boleh lompat
        if (phaseManager.currentPhase == PhaseManager.Phase.Red && Input.GetButtonDown("Jump"))
        {
            redTimer += Time.deltaTime;

            if (redTimer >= redTolerance)
            {
                Die();
                return;
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void CheckRedPhase()
    {
        if (phaseManager.currentPhase != PhaseManager.Phase.Red)
        {
            redTimer = 0f;
        }
    }

    void Die()
    {
        transform.position = spawnPoint.position;
        rb.velocity = Vector2.zero;
        yellowTimer = 0f;
        redTimer = 0f;
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}