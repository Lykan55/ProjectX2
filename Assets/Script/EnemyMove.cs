using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMove : MonoBehaviour
{
    GameObject Player;

    Vector3 TargetPos;
    int[,] Map;
    int[] MapPos;

    public List<Vector3> PosList = new List<Vector3>();
    public bool Return = false;

    void Start()
    {
        Player = GameObject.Find("Human 1");
        Map = GameObject.Find("StageMaker").GetComponent<stage>().ReturnMap();
        MapPos = MapPosSearch();
        PosList.Add(transform.position);
    }

    void Update()
    {


        transform.position = Vector3.MoveTowards(transform.position, TargetPos, 0.1f);
    }

    void TargetPosSearch()
    {

    }
    void FrontSearch()
    {
        int[] Data = { 1, 1, 1, 1 };

        if (Map[MapPos[0], MapPos[1] + 1] == 0)
        {
            Data[0] = 0;
        }
        if (Map[MapPos[0] + 1, MapPos[1]] == 0)
        {
            Data[1] = 0;
        }
        if (Map[MapPos[0], MapPos[1] - 1] == 0)
        {
            Data[2] = 0;
        }
        if (Map[MapPos[0] - 1, MapPos[1]] == 0)
        {
            Data[3] = 0;
        }

        while (true)
        {

        }
    }

    int[] MapPosSearch()
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
