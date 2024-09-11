using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class Human1 : MonoBehaviour
{
    [SerializeField, Header("移動速度")]//Unity上でMoveSpeedの値を変更できるようになる
    private float MoveSpeed;
    [SerializeField, Header("ジャンプ速度")]
    private float JumpSpeed;

    public GameObject PlayerCollision;// Human1の当たっているオブジェクト

    private Vector2 inputDirection;
    private Rigidbody2D rigid;
    private Animator anim;
    private bool bJump;
    private float MoveX = 0.0f;//向きの判定変数

    public float jumpForce = 10f;
    private int health = 1;

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
        if (MoveX > 0.0f)
        {
            transform.localScale = new Vector3(1, 1);
        }
        else if (MoveX < 0.0f)
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
        if (collision.gameObject.tag == "Floor" && collision.transform.position.y < transform.position.y - 3)
        {
            bJump = false;   //もう一度ジャンプできるように
            anim.SetBool("Jump", bJump);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            HandleCollisionWithEnemy(collision);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerCollision = collision.gameObject;
    }

    public void HandleCollisionWithEnemy(Collision2D collision)
    {
        float playerY = transform.position.y - 3;
        float contactY = collision.transform.position.y;

        TakeDamege();
        /*
        if (playerY > contactY)
        {
            Destroy(collision.gameObject);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpForce);
        }
        else
        {
            TakeDamege();
        }
        */
    }

    private void TakeDamege()
    {
        health -= 1;

        if (health <= 0)
        {
            //Debug.Log("Player is dead!");
            gameObject.SetActive(false);
            SceneManager.LoadScene("Dead");
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
            return; //キーが押されていない時に下の処理が実行されない命令
        }

        rigid.AddForce(Vector2.up * JumpSpeed, ForceMode2D.Impulse);//AddForce:加速処理
        bJump = true;
        anim.SetBool("Jump", bJump);
    }
}
