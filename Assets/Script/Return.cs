using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Return : MonoBehaviour
{
    /*
    public int ReturnCnt;

    private GameObject Player;
    private GameObject Enemy;

    private List<TimeList> PlayerList;
    private List<Vector3> EnemyList;

    private int PLCnt;
    private int ELCnt;
    private int PPosCnt;
    private int EPosCnt;

    private int PlayerEndCnt;
    private bool EnemyEndCnt = false;

    private Vector3 PlayerTargetPos;
    private Vector3 EnemyTargetPos;

    private bool PlayerFlag = false;
    private bool EnemyFlag = true;

    private void Start()
    {
        Player = GameObject.Find("Human 1");

        Player.GetComponent<PlayerData>().Return = true;
        Player.GetComponent<TimeWarp>().Return = true;

        PlayerList = new List<TimeList>(Player.GetComponent<TimeWarp>().Timelist);

        PLCnt = PlayerList.Count - 1;
        PPosCnt = PlayerList[PLCnt].Poslist.Count - 1;

        PlayerTargetPos = PlayerList[PLCnt].Poslist[PPosCnt];

        PlayerEndCnt = ReturnCnt + 1;

        if (PLCnt + 1 < PlayerEndCnt)
        {
            PlayerEndCnt = PLCnt + 1;
        }

        try
        {
            Enemy = GameObject.Find("enemy 0(Clone)");

            Enemy.GetComponent<EnemyData>().Return = true;

            EnemyList = new List<Vector3>(Enemy.GetComponent<EnemyData>().PosList);
            EnemyList.RemoveAt(EnemyList.Count - 1);

            ELCnt = EnemyList.Count - 1;
            EPosCnt = ReturnCnt;

            if (ELCnt >= 0)
            {
                EnemyTargetPos = EnemyList[ELCnt];
            }

            if (ELCnt + 1 < EPosCnt)
            {
                EPosCnt = ELCnt + 1;
                EnemyEndCnt = true;
            }
        }
        catch
        {
            EnemyFlag = false;
        }
    }
    private void Update()
    {
        EnemyMove();
        PlayerMove();

        if (PlayerFlag)
        {
            GameObject.Find("Panel").GetComponent<ItemManager>().ItemCheck = true;
            Destroy(this.gameObject);
        }
    }

    private void PlayerMove()
    {
        Player.transform.position = Vector3.MoveTowards(Player.transform.position, PlayerTargetPos, 0.5f);

        if (Player.transform.position == PlayerTargetPos)
        {
            PlayerList[PLCnt].Poslist.RemoveAt(PPosCnt);

            if (PPosCnt == 0)
            {
                PlayerEndCnt--;
                if (PlayerEndCnt > 0)
                {
                    PlayerList.RemoveAt(PLCnt);
                    PLCnt--;
                }
                else if (PlayerEndCnt == 0)
                {
                    if (EnemyFlag)
                    {
                        Enemy.GetComponent<EnemyData>().PosList = new List<Vector3>(EnemyList);
                        Enemy.GetComponent<EnemyData>().SetData();
                        Enemy.GetComponent<EnemyTimer>().Timer = 0;
                        Enemy.GetComponent<EnemyData>().Return = false;
                    }

                    PlayerList[PLCnt].Poslist.Add(Player.transform.position);
                    Player.GetComponent<TimeWarp>().Timelist = new List<TimeList>(PlayerList);
                    Player.GetComponent<PlayerData>().SetData();
                    Player.GetComponent<PlayerData>().Return = false;
                    Player.GetComponent<TimeWarp>().Return = false;

                    PlayerFlag = true;
                }
            }

            if (!PlayerFlag)
            {
                PPosCnt = PlayerList[PLCnt].Poslist.Count - 1;
                PlayerTargetPos = PlayerList[PLCnt].Poslist[PPosCnt];
            }
        }
    }

    private void EnemyMove()
    {
        if (EnemyFlag)
        {
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, EnemyTargetPos, 1.0f);

            if (Enemy.transform.position == EnemyTargetPos)
            {
                EPosCnt--;
                ELCnt--;

                if (EPosCnt > 0)
                {
                    EnemyList.RemoveAt(ELCnt + 1);
                    EnemyTargetPos = EnemyList[ELCnt];
                }
            }

            if (ELCnt < 0 && EnemyEndCnt)
            {
                EnemyFlag = false;
            }
        }
    }
    */

    private GameObject Player;
    private List<GameObject> Enemy;

    private List<Vector2> PlayerPosList;
    private List<List<Vector2>> EnemyPosList = new List<List<Vector2>>();

    private float Timer = 0.0f;
    private bool Flag = true;

    public float ReturnTime;
    public float ReturnSpeed;

    private void Start()
    {
        Player = GameObject.Find("Human 1");
        Enemy = new List<GameObject>(GameObject.Find("EnemyMaker").GetComponent<EnemyMaker>().ReturnEnemys());

        PlayerPosList = new List<Vector2>(Player.GetComponent<TimeWarpData>().PosList);
        Player.GetComponent<TimeWarpData>().Return = true;

        for (int i = 0; i < Enemy.Count; i++)
        {
            EnemyPosList.Add(new List<Vector2>(Enemy[i].GetComponent<TimeWarpData>().PosList));
            Enemy[i].GetComponent<TimeWarpData>().Return = true;
            Enemy[i].GetComponent<EnemyMove>().Return = true;
        }
    }

    private void Update()
    {
        if (Flag)
        {
            if (PlayerPosList.Count != 1)
            {
                Player.transform.position = Vector2.MoveTowards(Player.transform.position, PlayerPosList[PlayerPosList.Count - 1], ReturnSpeed);
                PlayerPosList.RemoveAt(PlayerPosList.Count - 1);
            }
            for (int i = 0; i < Enemy.Count; i++)
            {
                if (EnemyPosList[i].Count != 1)
                {
                    Enemy[i].transform.position = Vector2.MoveTowards(Enemy[i].transform.position, EnemyPosList[i][EnemyPosList[i].Count - 1], ReturnSpeed);
                    EnemyPosList[i].RemoveAt(EnemyPosList[i].Count - 1);
                }
            }


            if (Timer >= ReturnTime || PlayerPosList.Count == 1)
            {
                Player.GetComponent<TimeWarpData>().PosList = new List<Vector2>(PlayerPosList);
                Player.GetComponent<TimeWarpData>().Return = false;

                for (int i = 0; i < Enemy.Count; i++)
                {
                    Enemy[i].GetComponent<TimeWarpData>().PosList = new List<Vector2>(EnemyPosList[i]);
                    Enemy[i].GetComponent<EnemyMove>().RouteSeach();
                    Enemy[i].GetComponent<TimeWarpData>().Return = false;
                    Enemy[i].GetComponent<EnemyMove>().Return = false;
                }

                GameObject.Find("ItemPanel").GetComponent<ItemManager>().ItemCheck = true;
                Destroy(gameObject);
                Flag = false;
            }

            Timer += Time.deltaTime;
        }

    }
}
