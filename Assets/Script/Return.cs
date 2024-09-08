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

    private int PLCnt;
    private int ELCnt;
    private int PosCnt;

    private Vector3 TargetPos;

    private bool Flag = false;

    private void Start()
    {
        Player = GameObject.Find("Human 1");
        Enemy = GameObject.Find("enemy 0(Clone)");

        Player.GetComponent<PlayerData>().Return = true;
        Player.GetComponent<TimeWarp>().Return = true;
        Enemy.GetComponent<EnemyData>().Return = true;

        PlayerList = Player.GetComponent<TimeWarp>().TimeList;
        EnemyList = Enemy.GetComponent<EnemyData>().PosList;
        EnemyList.RemoveAt(EnemyList.Count - 1);

        PLCnt = PlayerList.Length - 1;
        ELCnt = EnemyList.Count - 1;
        PosCnt = PlayerList[PLCnt].Poslist.Count - 1;

        TargetPos = PlayerList[PLCnt].Poslist[PosCnt];
    }
    private void Update()
    {
        EnemyMove();
        PlayerMove();

        if (Flag)
        {
            Destroy(this.gameObject);
        }
    }

    private void PlayerMove()
    {
        Player.transform.position = Vector3.MoveTowards(Player.transform.position, TargetPos, 0.5f);

        if (Player.transform.position == TargetPos)
        {
            PlayerList[PLCnt].Poslist.RemoveAt(PosCnt);

            if (PosCnt == 0)
            {
                if (PLCnt == 0)
                {
                    Player.GetComponent<TimeWarp>().TimeList = PlayerList;
                    Enemy.GetComponent<EnemyData>().PosList = EnemyList;
                    Player.GetComponent<PlayerData>().Return = false;
                    Player.GetComponent<TimeWarp>().Return = false;
                    Enemy.GetComponent<EnemyData>().Return = false;
                    Flag = true;
                    Debug.Log("a");
                }
                else
                {
                    Debug.Log("S");
                    ELCnt--;
                    PLCnt--;
                    EnemyList.RemoveAt(EnemyList.Count - 1);
                }
            }

            if (!Flag)
            {
                PosCnt = PlayerList[PLCnt].Poslist.Count - 1;
                Debug.Log($"PosCnt:{PosCnt}");
                TargetPos = PlayerList[PLCnt].Poslist[PosCnt];
                Debug.Log($"PLCnt:{PLCnt}");
            }
        }
    }

    private void EnemyMove()
    {
        if (ELCnt >= 0)
        {
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, EnemyList[ELCnt], 1.0f);
        }
    }
}
