using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMove : MonoBehaviour
{
    GameObject Player;
    Vector3 TargetPos;
    int[,] Map;
    public int[] TargetMapPos = new int[2];
    public int Front = 0;
    private bool SearchFlag = false;

    public bool Return = false;

    void Start()
    {
        Player = GameObject.Find("Human 1");
        Map = GameObject.Find("StageMaker").GetComponent<stage>().ReturnMap();
        TargetMapPos = GameObject.Find("StageMaker").GetComponent<stage>().ReturnEnemyPos();
        TargetPos.x = TargetMapPos[0] * 20;
        TargetPos.y = TargetMapPos[1] * 20;
    }

    void Update()
    {
        if (!Return)
        {
            PlayerPosSearch();
            transform.position = Vector3.MoveTowards(transform.position, TargetPos, 0.1f);

            if (TargetPos == transform.position)
            {
                Front = FrontSearch();
                TargetPosSearch();
                TargetPos.x = TargetMapPos[0] * 20;
                TargetPos.y = TargetMapPos[1] * 20;
            }
        }
    }

    void TargetPosSearch()
    {
        bool Flag = true;

        while (AreaSearch()[Front] == 1 && Flag)
        {
            switch (Front)
            {
                case 0:
                    TargetMapPos[1]++;
                    break;
                case 1:
                    TargetMapPos[0]++;
                    break;
                case 2:
                    TargetMapPos[1]--;
                    break;
                case 3:
                    TargetMapPos[0]--;
                    break;
            }

            if (SideSearch())
            {
                Flag = false;
            }
        }
    }
    int[] AreaSearch()
    {
        int[] Data = { 1, 1, 1, 1, 4 };

        if (Map[TargetMapPos[0], TargetMapPos[1] + 1] == 0)
        {
            Data[0] = 0;
            Data[4]--;
        }
        if (Map[TargetMapPos[0] + 1, TargetMapPos[1]] == 0)
        {
            Data[1] = 0;
            Data[4]--;
        }
        if (Map[TargetMapPos[0], TargetMapPos[1] - 1] == 0)
        {
            Data[2] = 0;
            Data[4]--;
        }
        if (Map[TargetMapPos[0] - 1, TargetMapPos[1]] == 0)
        {
            Data[3] = 0;
            Data[4]--;
        }

        return Data;
    }
    int FrontSearch()
    {
        int[] Data = AreaSearch();
        int DataFront = 0;
        if (Data[4] > 1)
        {
            Front += 2;
            if (Front > 3)
            {
                Front -= 4;
            }

            do
            {
                DataFront = Random.Range(0, 4);
            }
            while (Data[DataFront] == 0 || DataFront == Front);
        }
        else
        {
            while (Data[DataFront] == 0)
            {
                DataFront++;
            }
        }

        return DataFront;
    }
    bool SideSearch()
    {
        int[] Data = AreaSearch();
        int[] Side = { 0, 0 };

        Side[0] = Front - 1;
        Side[1] = Front + 1;

        for (int n = 0; n < 2; n++)
        {
            if (Side[n] < 0)
            {
                Side[n] = 3;
            }
            else if (3 < Side[n])
            {
                Side[n] = 0;
            }

            if (Data[Side[n]] == 1)
            {
                return true;
            }
        }

        return false;
    }

    void PlayerPosSearch()
    {
        Vector3 PlayerPos = Player.transform.position;
        float PosX = transform.position.x - PlayerPos.x;
        float PosY = transform.position.y - PlayerPos.y;

        bool Flag = false;

        int SearchAria;
        if (SearchFlag)
        {
            SearchAria = 50;
        }
        else
        {
            SearchAria = 30;
        }

        switch (Front)
        {
            case 0:
                if (SearchAria * -1 <= PosY && PosY <= 0 && Mathf.Abs(PosX) <= 10)
                {
                    Flag = true;
                }
                break;
            case 1:
                if (SearchAria * -1 <= PosX && PosX <= 0 && Mathf.Abs(PosY) <= 10)
                {
                    Flag = true;
                }
                break;
            case 2:
                if (0 <= PosY && PosY <= SearchAria && Mathf.Abs(PosX) <= 10)
                {
                    Flag = true;
                }
                break;
            case 3:
                if (0 <= PosX && PosX <= SearchAria && Mathf.Abs(PosY) <= 10)
                {
                    Flag = true;
                }
                break;
        }

        if (Flag)
        {
            MapPosLoad(PlayerPos);
            SearchFlag = true;
        }
        else
        {
            SearchFlag = false;
        }
    }
    void MapPosLoad(Vector3 PlayerPos)
    {
        TargetMapPos[0] = (int)PlayerPos.x / 20;
        if (PlayerPos.x % 20 > 10)
        {
            TargetMapPos[0]++;
        }

        TargetMapPos[1] = (int)PlayerPos.y / 20;
        if (PlayerPos.y % 20 > 10)
        {
            TargetMapPos[1]++;
        }

        TargetPos.x = TargetMapPos[0] * 20;
        TargetPos.y = TargetMapPos[1] * 20;
    }

    public void MapNewLoad(Vector2 Pos)
    {
        int x = (int)Pos.x / 20;
        if (Pos.x % 20 > 10)
        {
            x++;
        }

        int y = (int)Pos.y / 20;
        if (Pos.y % 20 > 10)
        {
            y++;
        }

        int[] NewPos = GameObject.Find("StageMaker").GetComponent<stage>().ReturnPos(x, y);

        Map[NewPos[0], NewPos[1]] = 1;
    }

    public void RouteSeach(Vector2 PrePos)
    {
        if (PrePos.x == transform.position.x)
        {
            if (PrePos.y > transform.position.y)
            {
                Front = 0;
            }
            else
            {
                Front = 2;
            }
        }
        else
        {
            if (PrePos.x > transform.position.x)
            {
                Front = 1;
            }
            else
            {
                Front = 3;
            }
        }

        TargetPosSet();
        TargetPosSearch();
        TargetPos.x = TargetMapPos[0] * 20;
        TargetPos.y = TargetMapPos[1] * 20;
    }
    private void TargetPosSet()
    {
        int x = (int)transform.position.x / 20;
        if (transform.position.x % 20 > 10)
        {
            x++;
        }

        int y = (int)transform.position.y / 20;
        if (transform.position.y % 20 > 10)
        {
            y++;
        }

        TargetMapPos[0] = x;
        TargetMapPos[1] = y;
    }
}
