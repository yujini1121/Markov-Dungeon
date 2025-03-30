using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

/* 
    이 스크립트는 플레이어의 이동을 제어하는 역할을 합니다.  
    WASD 및 방향키 입력을 받아 이동하며,  
    특정 상황(다음 방으로 이동할 때)에서 이동을 제한할 수 있습니다.
*/

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    public bool canMove = true;
    private bool facingRight = true;

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
            animator.SetBool("isRunning", movement.sqrMagnitude > 0);

            // 캐릭터 방향 회전
            if (moveX > 0 && !facingRight)
            {
                Flip();
            }
            else if (moveX < 0 && facingRight)
            {
                Flip();
            }
        }
        else
        {
            movement = Vector2.zero;
            animator.SetBool("isRunning", false);
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

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
