using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Return : MonoBehaviour
{
    public int ReturnCnt;

    private GameObject Player;
    private GameObject Enemy;

    private List<TimeList> PlayerList;
    private List<Vector3> EnemyList;

    private int PLCnt;
    private int ELCnt;
    private int PPosCnt;
    private int EPosCnt;

    private int EndCnt;

    private Vector3 TargetPos;

    private bool Flag = false;
    private bool Go = false;

    private void Start()
    {
        Player = GameObject.Find("Human 1");

        Player.GetComponent<PlayerData>().Return = true;
        Player.GetComponent<TimeWarp>().Return = true;

        PlayerList = new List<TimeList>(Player.GetComponent<TimeWarp>().Timelist);

        PLCnt = PlayerList.Count - 1;
        PPosCnt = PlayerList[PLCnt].Poslist.Count - 1;

        TargetPos = PlayerList[PLCnt].Poslist[PPosCnt];

        EndCnt = ReturnCnt + 1;

        if (PLCnt + 1 < EndCnt)
        {
            EndCnt = PLCnt + 1;
        }

        try
        {
            Enemy = GameObject.Find("enemy 0(Clone)");

            Enemy.GetComponent<EnemyData>().Return = true;

            EnemyList = new List<Vector3>(Enemy.GetComponent<EnemyData>().PosList);
            EnemyList.RemoveAt(EnemyList.Count - 1);

            ELCnt = EnemyList.Count - 1;
            EPosCnt = ReturnCnt;
            Debug.Log($"Farst ELCnt:{ELCnt}");
            Debug.Log($"Farst ELCnt:{ELCnt}");
            Debug.Log($"Farst EPosCnt:{EPosCnt}");
        }
        catch
        {

        }
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

        if (Player.transform.position == TargetPos && Go)
        {
            Go = false;

            PlayerList[PLCnt].Poslist.RemoveAt(PPosCnt);

            if (PPosCnt == 0)
            {
                try
                {
                    EndCnt--;
                    PLCnt--;
                    EPosCnt--;
                    Debug.Log($"ELCnt:{ELCnt}");
                    Debug.Log($"EPosCnt:{EPosCnt}");
                }
                catch
                {

                }

                if (EPosCnt > 0)
                {
                    try
                    {
                    ELCnt--;
                    EnemyList.RemoveAt(EnemyList.Count - 1);
                    Debug.Log($"ELCnt:{ELCnt}");
                    Debug.Log($"EPosCnt:{EPosCnt}");
                    }
                    catch
                    {

                    }
                }

                if (EndCnt == 0)
                {
                    try
                    {
                        Enemy.GetComponent<EnemyData>().PosList = new List<Vector3>(EnemyList);
                        Debug.Log("Try PosList");
                        Enemy.GetComponent<EnemyData>().SetData();
                        Debug.Log("SetNum");
                        Enemy.GetComponent<EnemyTimer>().Timer = 0;
                        Enemy.GetComponent<EnemyData>().Return = false;
                        Debug.Log("Return");
                    }
                    catch
                    {

                    }

                    Player.GetComponent<TimeWarp>().Timelist = new List<TimeList>(PlayerList);
                    Player.GetComponent<PlayerData>().SetData();
                    Player.GetComponent<PlayerData>().Return = false;
                    Player.GetComponent<TimeWarp>().Return = false;

                    Flag = true;
                }
            }

            if (!Flag)
            {
                PPosCnt = PlayerList[PLCnt].Poslist.Count - 1;
                TargetPos = PlayerList[PLCnt].Poslist[PPosCnt];
            }
        }
    }

    private void EnemyMove()
    {
        if (EPosCnt > 0 && ELCnt >= 0)
        {
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, EnemyList[ELCnt], 1.0f);

            if (Enemy.transform.position == EnemyList[ELCnt])
            {
                Go = true;
            }
        }
        else
        {
            Go = true;
        }

        if (ELCnt < 0)
        {
            Player.GetComponent<PlayerData>().SummonFlag();
            Destroy(Enemy);
        }
    }
}
