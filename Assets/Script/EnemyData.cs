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
    public List<Vector3> PosList = new List<Vector3>();
    private int ListNum = 0;
    public bool Return = false;

    private void Start()
    {
        Player = GameObject.Find("Human 1");
        EnemyCurrPos = GetComponent<Transform>().position;
        EnemyPrePos = EnemyCurrPos;
        PosList.Add(EnemyCurrPos);
        Player.GetComponent<PlayerData>().Movement = false;
    }

    private void Update()
    {
        if (!Return)
        {
            Directions();

            if (Player.GetComponent<PlayerData>().Movement == true)
            {
                PosList.Add(Player.GetComponent<PlayerData>().PlayerPrePos);
                GetComponent<EnemyTimer>().Timer = 0.0f;
                Player.GetComponent<PlayerData>().Movement = false;
            }
            else if (GetComponent<EnemyTimer>().Timer >= 5.0f)
            {
                PosList.Add(Player.GetComponent<PlayerData>().PlayerCurrPos);
                GetComponent<EnemyTimer>().Timer = 0.0f;
                Player.GetComponent<TimeWarp>().CntControl();
            }

            Move();
        }

    }

    private void Directions()
    {
        Vector3 localScale = transform.localScale;
        if (localScale.x < 0 && Player.transform.position.x < transform.position.x || localScale.x > 0 && transform.position.x < Player.transform.position.x)
        {
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private void Move()
    {
        var rate = (Time.timeSinceLevelLoad - StartTime) / 1;

        transform.position = Vector3.Lerp(EnemyPrePos, EnemyCurrPos, rate);

        if (transform.position == EnemyCurrPos && ListNum < PosList.Count - 1)
        {
            ListNum++;
            EnemyPrePos = EnemyCurrPos;
            EnemyCurrPos = PosList[ListNum];
            StartTime = Time.timeSinceLevelLoad;
        }
    }

    public void SetData()
    {
        ListNum = PosList.Count - 1;
        EnemyCurrPos = PosList[ListNum];
        EnemyPrePos = EnemyCurrPos;
    }
}
