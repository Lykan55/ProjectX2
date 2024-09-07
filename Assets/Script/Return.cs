using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Return : MonoBehaviour
{
    private GameObject Player;
    private GameObject Enemy;

    private TimeList[] PlayerList;
    private List<Vector3> EnemyList;

    private float StartTime;
    private int Cnt;
    private int PosCnt;

    private Vector3 CurrPos;
    private Vector3 PrePos;

    private void Start()
    {
        Player = GameObject.Find("Human 1");
        Enemy = GameObject.Find("enemy 0");

        PlayerList = Player.GetComponent<TimeWarp>().TimeList;
        EnemyList = Enemy.GetComponent<EnemyData>().PosList;

        Cnt = PlayerList.Length - 1;
        PosCnt = PlayerList[Cnt].Poslist.Count - 1;

        Player.GetComponent<PlayerData>().Return = true;
        Player.GetComponent<TimeWarp>().Return = true;

        StartTime = Time.timeSinceLevelLoad;

        CurrPos = PlayerList[Cnt].Poslist[PosCnt];
        PrePos = PlayerList[Cnt].Poslist[PosCnt - 1];
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var rate = (Time.timeSinceLevelLoad - StartTime) / 0.1f;

        Player.GetComponent<Transform>().position = Vector3.Lerp(CurrPos, PrePos, rate);

        if (Player.transform.position == PrePos)
        {
            PlayerList[Cnt].Poslist.RemoveAt(PosCnt);
            PosCnt = PlayerList[Cnt].Poslist.Count - 1;
            if (PosCnt == 0)
            {
                if (Cnt == 0)
                {
                    Player.GetComponent<TimeWarp>().TimeList = PlayerList;
                    Player.GetComponent<PlayerData>().Return = false;
                    Player.GetComponent<TimeWarp>().Return = false;
                    Destroy(this.gameObject);
                }
                else
                {
                    Cnt--;
                }
            }
            CurrPos = PrePos;
            PrePos = PlayerList[Cnt].Poslist[PosCnt - 1];
            StartTime = Time.timeSinceLevelLoad;
        }
    }
}
