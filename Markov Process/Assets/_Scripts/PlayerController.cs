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
            // �̵� �Է� �ޱ� (WASD �Ǵ� ����Ű)
            float moveX = UnityEngine.Input.GetAxisRaw("Horizontal");
            float moveY = UnityEngine.Input.GetAxisRaw("Vertical");

            // �Է¿� ���� ���� ���
            movement = new Vector2(moveX, moveY).normalized;

            //// �ִϸ��̼� ����
            //if (Mathf.Abs(moveX) > Mathf.Epsilon)  // �����̰� �ִٸ�
            //{
            //    animator.SetInteger("AnimState", 1);  // Run ����
            //}
            //else  // ����ٸ�
            //{
            //    animator.SetInteger("AnimState", 0);  // Idle ����
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
