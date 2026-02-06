using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 8f;
    private float jumpForce = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private bool isGrounded = true;

    private Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start() { }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        HandleMovement();
        HandleJump();
        HandleCombat();
    }

    private void HandleJump()
    {
        animator.SetBool("IsGrounded", isGrounded);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0) transform.localScale = new Vector3(-5, 4, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(5, 4, 1);

        animator.SetBool("IsRunning", moveInput != 0);
    }

    private void HandleCombat()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Attack");
        }
    }

    // Thêm biến này vào PlayerController
    public int stressTestAmount = 500; // Số lượng đủ lớn để gây khựng hình

    private void HandleCombat1()
    {
        // ... logic Z (tấn công dùng pool) giữ nguyên ...

        // TÌNH HUỐNG GIẢ LAG: Nhấn phím L
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Stress Test: Đang ép CPU cấp phát bộ nhớ rác...");
            for (int i = 0; i < stressTestAmount; i++)
            {
                // 1. KHÔNG dùng Pool: Ép Unity tìm vùng nhớ mới, nạp Prefab, khởi tạo Component
                GameObject trash = Instantiate(ObjectPool.Instance.enemyPrefab, transform.position + (Vector3)Random.insideUnitCircle * 10, Quaternion.identity);

                // 2. KHÔNG dùng Return: Xóa ngay lập tức để tạo "Rác" (Garbage)
                // Việc Destroy hàng loạt sẽ bắt Garbage Collector phải chạy, gây ra hiện tượng khựng (Spike)
                Destroy(trash, 0.05f);
            }
        }
    }
}
