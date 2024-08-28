using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy0 : MonoBehaviour
{
    [SerializeField, Header("移動速度")]//Unity上でMoveSpeedの値を変更できるようになる

    //private float MoveSpeed;
    public float speed = 2f;  // 敵キャラクターの移動速度
    private Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }

    private void Move()
    {
        rigid.velocity = new Vector2(-speed, rigid.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // wallタグのオブジェクトに衝突した場合
        if (collision.gameObject.tag == "wall")
        {
            // 移動方向を反転させる
            speed = -speed;

            // 敵キャラクターの向きを反転させる
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
