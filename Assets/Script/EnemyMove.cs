using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMove : MonoBehaviour
{
    GameObject Player;

    Vector3 TargetPos;
    int[,] Map;
    int[] TargetMapPos;
    public int Front = 0;

    void Start()
    {
        Player = GameObject.Find("Human 1");
        Map = GameObject.Find("StageMaker").GetComponent<stage>().ReturnMap();
        TargetMapPos = FirstPosSearch();
        TargetPos.x = TargetMapPos[0] * 20;
        TargetPos.y = TargetMapPos[1] * 20;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, 0.1f);

        if (TargetPos == transform.position)
        {
            Front = FrontSearch();
            TargetPosSearch();
            TargetPos.x = TargetMapPos[0] * 20;
            TargetPos.y = TargetMapPos[1] * 20;
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

        Flag = true;
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
        Debug.Log($"{Data[4]}");
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

    int[] FirstPosSearch()
    {
        int[] ans = { 0, 0 };

        for (int x = 0; x < Map.GetLength(0); x++)
        {
            for (int y = 0; y < Map.GetLength(1); y++)
            {
                if (Map[x, y] == 3)
                {
                    ans[0] = x;
                    ans[1] = y;
                    return ans;
                }
            }
        }

        return ans;
    }
}
