using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 이동 입력 받기 (WASD 또는 방향키)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // 입력에 따른 방향 계산
        movement = new Vector2(moveX, moveY).normalized;

        // 애니메이션 설정
        if (moveX != 0 && moveY != 0)
        {
            animator.SetInteger("AnimState", 1);
        }
        else
        {
            animator.SetInteger("AnimState", 0);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }
}
