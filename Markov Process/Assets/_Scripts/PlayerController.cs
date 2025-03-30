using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

/* 
    �� ��ũ��Ʈ�� �÷��̾��� �̵��� �����ϴ� ������ �մϴ�.  
    WASD �� ����Ű �Է��� �޾� �̵��ϸ�,  
    Ư�� ��Ȳ(���� ������ �̵��� ��)���� �̵��� ������ �� �ֽ��ϴ�.
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
            // �̵� �Է� �ޱ� (WASD �Ǵ� ����Ű)
            float moveX = UnityEngine.Input.GetAxisRaw("Horizontal");
            float moveY = UnityEngine.Input.GetAxisRaw("Vertical");

            // �Է¿� ���� ���� ���
            movement = new Vector2(moveX, moveY).normalized;
            animator.SetBool("isRunning", movement.sqrMagnitude > 0);

            // ĳ���� ���� ȸ��
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
