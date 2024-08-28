using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemy0 : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")]//Unity���MoveSpeed�̒l��ύX�ł���悤�ɂȂ�

    //private float MoveSpeed;
    public float speed = 2f;  // �G�L�����N�^�[�̈ړ����x
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
        // wall�^�O�̃I�u�W�F�N�g�ɏՓ˂����ꍇ
        if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "Enemy")
        {
            // �ړ������𔽓]������
            speed = -speed;

            // �G�L�����N�^�[�̌����𔽓]������
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        // wall�^�O�̃I�u�W�F�N�g�ɏՓ˂����ꍇ
        if (trigger.gameObject.tag == "wall")
        {
            // �ړ������𔽓]������
            speed = -speed;

            // �G�L�����N�^�[�̌����𔽓]������
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
