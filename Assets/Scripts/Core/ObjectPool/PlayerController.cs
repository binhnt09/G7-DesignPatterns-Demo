using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 8f;
    private float jumpForce = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private bool isGrounded;

    private Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0) transform.localScale = new Vector3(-5, 4, 1);
        if (moveInput < 0) transform.localScale = new Vector3(5, 4, 1);

        if (moveInput == 0) animator.SetBool("IsRunning", false);
        else animator.SetBool("IsRunning", true);

        AttackAndJump();
    }
    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void AttackAndJump()
    {
        animator.SetBool("IsGrounded", isGrounded);
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetButtonDown("Jump")) && isGrounded)
        {
            animator.SetTrigger("Jump");
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Attack");
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("IsGrounded", true);
        }
    }
}
