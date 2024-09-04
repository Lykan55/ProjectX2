using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public Vector3 EnemyCurrPos = new Vector2();
    public Vector3 EnemyPrePos = new Vector2();
    GameObject Player;


    private void Start()
    {
        Player = GameObject.Find("Human 1");
        EnemyCurrPos = Player.GetComponent<PlayerData>().PlayerPrePos;
    }

    private void Update()
    {
        Directions();

        if (Player.GetComponent<PlayerData>().Movement == true)
        {
            if (EnemyCurrPos != Player.GetComponent<PlayerData>().PlayerPrePos)
            {
                EnemyPrePos = EnemyCurrPos;
                EnemyCurrPos = Player.GetComponent<PlayerData>().PlayerPrePos;
            }

            Invoke(nameof(Move), 0.5f);

            if (EnemyCurrPos == transform.position)
            {
                Player.GetComponent<PlayerData>().Movement = false;
            }
        }
    }

    private void Move()
    {
        Vector3 pos = transform.position;

        int Ans = Destination();
        switch (Ans)
        {
            case 12:
                pos.y += 0.5f;
                break;
            case 3:
                pos.x += 0.5f;
                break;
            case 6:
                pos.y -= 0.5f;
                break;
            case 9:
                pos.x -= 0.5f;
                break;
        }

        transform.position = pos;
    }

    private int Destination()
    {
        int Ans;

        if (EnemyCurrPos.x == transform.position.x)
        {
            if (EnemyCurrPos.y > transform.position.y)
            {
                Ans = 12;
            }
            else
            {
                Ans = 6;
            }
        }
        else
        {
            if (EnemyCurrPos.x > transform.position.x)
            {
                Ans = 3;
            }
            else
            {
                Ans = 9;
            }
        }

        return Ans;
    }

    private void Directions()
    {
        Vector3 localScale = transform.localScale;
        if (localScale.x < 0 && Player.transform.position.x < transform.position.x)
        {
            localScale.x *= -1;
            transform.localScale = localScale;
        }
        else if (localScale.x > 0 && transform.position.x < Player.transform.position.x)
        {
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
