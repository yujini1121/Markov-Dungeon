using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    public bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove)
        {
            // 이동 입력 받기 (WASD 또는 방향키)
            float moveX = UnityEngine.Input.GetAxisRaw("Horizontal");
            float moveY = UnityEngine.Input.GetAxisRaw("Vertical");

            // 입력에 따른 방향 계산
            movement = new Vector2(moveX, moveY).normalized;

            //// 애니메이션 설정
            //if (Mathf.Abs(moveX) > Mathf.Epsilon)  // 움직이고 있다면
            //{
            //    animator.SetInteger("AnimState", 1);  // Run 상태
            //}
            //else  // 멈췄다면
            //{
            //    animator.SetInteger("AnimState", 0);  // Idle 상태
            //}
        }
        else
        {
            movement = Vector2.zero;
        }
    }

    public void MoveControlSwitch(bool input, float yPos = 0f)
    {
        GetComponent<SpriteRenderer>().enabled = input;
        canMove = input;

        if (input)
        {
            transform.position = new Vector2(0f, yPos - 3f);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }
}
