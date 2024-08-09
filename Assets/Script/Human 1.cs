//���݂̌ċz
//���}��
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Human1 : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")]//Unity���MoveSpeed�̒l��ύX�ł���悤�ɂȂ�
    private float MoveSpeed;
    [SerializeField, Header("�W�����v���x")]
    private float JumpSpeed;

    private Vector2 inputDirection;
    private Rigidbody2D rigid;
    private Animator anim;
    private bool bJump;
    private float MoveX = 0.0f;//�����̔���ϐ�

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bJump = false;
    }

    void Update()
    {
        Move();
        MoveX = Input.GetAxisRaw("Horizontal");
        if(MoveX > 0.0f)
        {
            transform.localScale = new Vector3(1, 1);
        }
        else if(MoveX < 0.0f)
        {
            transform.localScale = new Vector3(-1, 1);
        }
    }

    private void Move()
    {
        rigid.velocity = new Vector2(inputDirection.x * MoveSpeed, rigid.velocity.y);
        anim.SetBool("walk", inputDirection.x != 0.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            bJump = false;   //������x�W�����v�ł���悤��
            anim.SetBool("Jump", bJump);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed || bJump)
        {
            return; //�L�[��������Ă��Ȃ����ɉ��̏��������s����Ȃ�����
        }
        rigid.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);//AddForce:��������
        bJump = true;
        anim.SetBool("Jump", bJump);
    }
}
