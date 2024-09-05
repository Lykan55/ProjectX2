using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public Vector3 EnemyCurrPos = new Vector2();
    public Vector3 EnemyPrePos = new Vector2();
    GameObject Player;
    private float StartTime;


    private void Start()
    {
        Player = GameObject.Find("Human 1");
        EnemyCurrPos = Player.GetComponent<PlayerData>().PlayerPrePos;
        EnemyPrePos = EnemyCurrPos;
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
                StartTime = Time.timeSinceLevelLoad;
            }
        }
        else if (GetComponent<EnemyTimer>().Timer >= 5)
        {
            if (EnemyCurrPos != Player.GetComponent<PlayerData>().PlayerCurrPos)
            {
                EnemyPrePos = EnemyCurrPos;
                EnemyCurrPos = Player.GetComponent<PlayerData>().PlayerCurrPos;
                StartTime = Time.timeSinceLevelLoad;
            }
        }

        Move();
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

    private void Move()
    {
        var rate = (Time.timeSinceLevelLoad - StartTime) / 1;

        transform.position = Vector3.Lerp(EnemyPrePos, EnemyCurrPos, rate);

        if (EnemyCurrPos == transform.position)
        {
            Player.GetComponent<PlayerData>().Movement = false;
        }
    }
}
