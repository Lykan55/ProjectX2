using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy0 : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")]//Unity���MoveSpeed�̒l��ύX�ł���悤�ɂȂ�
    private float MoveSpeed;

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
        rigid.velocity = new Vector2(Vector2.left.x * MoveSpeed, rigid.velocity.y);
    }
}
