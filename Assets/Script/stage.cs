using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class stage : MonoBehaviour
{
    private int x;
    private int y;
    private int[,] Stage;
    private int[] data = { -1, -1, -1, -1 };
    private int[] rist = new int[1];
    private int cnt = 0;
    private int go = -1;
    private bool end = false;
    private int[] start = new int[2];
    private int[] goaldata = { 0, 0, 0 };
    private List<int[]> EnemySummonData = new List<int[]>();
    private bool EnemyFlag = true;

    public int width;
    public int height;
    private int inputx;
    private int inputy;

    public int GemNumber = 0;

    public GameObject Enemy;

    public GameObject Gem;

    public GameObject stageblockA;
    public GameObject stageblockB;
    public GameObject stageblockC;

    void Start()
    {
        inputx = inputnumber(width);
        inputy = inputnumber(height);

        Stage = new int[inputx, inputy];

        //Reset();
        firstpoint();

        while (end == false)
        {
            go = -1;
            for (int a = 0; a < 4; a++)
            {
                data[a] = -1;
            }

            search();

            if (data[0] == -1 && data[1] == -1 && data[2] == -1 && data[3] == -1)
            {
                if (EnemyFlag)
                {
                    int[] SummonData = { inputx - 1 - x, inputy - 1 - y };
                    EnemySummonData.Add(SummonData);
                    EnemyFlag = false;
                }

                if (goaldata[2] < rist.Length)
                {
                    goaldata[0] = x;
                    goaldata[1] = y;
                    goaldata[2] = rist.Length;
                }

                nothing();
            }
            else
            {
                if (!EnemyFlag)
                {
                    EnemyFlag = true;
                }
                going();
            }
        }

        for (x = 1; x < inputx - 1; x++)
        {
            for (y = 1; y < inputy - 2; y++)
            {
                if (Stage[x, y] != 0 && Stage[x, y + 1] != 0)
                {
                    Stage[x, y + 1] = 2;
                }
            }
        }

        Vector2 goalpos = new Vector2((inputx - 1 - goaldata[0]) * 20, (inputy - 1 - goaldata[1]) * 20 - 7);
        GameObject.Find("Goal").GetComponent<Transform>().position = goalpos;

        for (y = 0; y < inputy; y++)
        {
            for (x = 0; x < inputx; x++)
            {
                if (Stage[x, y] == 0)
                {
                    Vector2 pos = new Vector2((inputx - 1 - x) * 20, (inputy - 1 - y) * 20);
                    GameObject boxA = Instantiate(stageblockA, pos, Quaternion.identity);
                    boxA.transform.parent = transform;

                    GameObject boxC = Instantiate(stageblockC, pos, Quaternion.identity);
                    boxC.transform.parent = transform;
                }
                else if (Stage[x, y] == 1)
                {
                    Vector2 pos = new Vector2((inputx - 1 - x) * 20, (inputy - 1 - y) * 20);
                    GameObject gem = Instantiate(Gem, pos, Quaternion.identity);
                    gem.transform.parent = transform;
                    GemNumber++;

                    GameObject boxC = Instantiate(stageblockC, pos, Quaternion.identity);
                    boxC.transform.parent = transform;
                    /*
                    if (x == 0 || x == inputx - 1 || y == 0 || y == inputy - 1)
                    {

                    }
                    */
                }
                else if (Stage[x, y] == 2)
                {
                    Vector2 pos = new Vector2((inputx - 1 - x) * 20, (inputy - 1 - y) * 20);
                    GameObject boxB = Instantiate(stageblockB, pos, Quaternion.identity);
                    boxB.transform.parent = transform;

                    GameObject gem = Instantiate(Gem, pos, Quaternion.identity);
                    gem.transform.parent = transform;
                    GemNumber++;

                    GameObject boxC = Instantiate(stageblockC, pos, Quaternion.identity);
                    boxC.transform.parent = transform;
                }
            }
        }

        GameObject.Find("EnemyMaker").GetComponent<EnemyMaker>().Summon(EnemySummonData);
    }





    int inputnumber(int input)
    {
        if (input < 5)
        {
            input = 5;
        }
        if (input % 2 == 0)
        {
            input++;
        }

        return input;
    }

    void firstpoint()
    {
        x = UnityEngine.Random.Range(0, inputx - 1);
        if (x % 2 == 0)
        {
            x++;
        }
        y = UnityEngine.Random.Range(0, inputy - 1);
        if (y % 2 == 0)
        {
            y++;
        }

        Stage[x, y] = 1;
        start[0] = x;
        start[1] = y;

        Vector2 pos = new Vector2((inputx - 1 - x) * 20, (inputy - 1 - y) * 20 - 7);
        GameObject.Find("Human 1").GetComponent<Transform>().position = pos;

    }

    void search()
    {
        for (int a = 0; a < 4; a++)
        {
            switch (a)
            {
                case 0:
                    try
                    {
                        if (Stage[x, y + 2] == 0)
                        {
                            data[0] = 0;
                        }
                    }
                    catch { data[0] = -1; }

                    break;
                case 1:
                    try
                    {
                        if (Stage[x + 2, y] == 0)
                        {
                            data[1] = 1;
                        }
                    }
                    catch { data[1] = -1; }

                    break;
                case 2:
                    try
                    {
                        if (Stage[x, y - 2] == 0)
                        {
                            data[2] = 2;
                        }
                    }
                    catch { data[2] = -1; }

                    break;
                case 3:
                    try
                    {
                        if (Stage[x - 2, y] == 0)
                        {
                            data[3] = 3;
                        }
                    }
                    catch { data[3] = -1; }

                    break;
            }
        }
    }

    void nothing()
    {
        if (cnt == 0)
        {
            end = true;
        }
        else
        {
            switch (rist[cnt - 1])
            {
                case 0:
                    y -= 2;
                    break;
                case 1:
                    x -= 2;
                    break;
                case 2:
                    y += 2;
                    break;
                case 3:
                    x += 2;
                    break;
            }

            cnt--;
            Array.Resize(ref rist, rist.Length - 2);
            Array.Resize(ref rist, rist.Length + 1);
        }
    }

    void going()
    {
        while (go == -1)
        {
            go = data[UnityEngine.Random.Range(0, 4)];
        }
        rist[cnt] = go;
        Array.Resize(ref rist, rist.Length + 1);
        cnt++;

        switch (go)
        {
            case 0:
                Stage[x, y + 1] = 1;
                Stage[x, y + 2] = 1;
                y += 2;
                break;
            case 1:
                Stage[x + 1, y] = 1;
                Stage[x + 2, y] = 1;
                x += 2;
                break;
            case 2:
                Stage[x, y - 1] = 1;
                Stage[x, y - 2] = 1;
                y -= 2;
                break;
            case 3:
                Stage[x - 1, y] = 1;
                Stage[x - 2, y] = 1;
                x -= 2;
                break;
        }
    }

    private void Reset()
    {
        for (y = 0; y < inputy; y++)
        {
            for (x = 0; x < inputx; x++)
            {
                Stage[x, y] = 0;
            }
        }

        end = false;

        for (int n = 0; n < 3; n++)
        {
            goaldata[n] = 0;
        }

        cnt = 0;

        Array.Resize(ref rist, 0);
        Array.Resize(ref rist, 1);
    }

    public int[,] ReturnMap()
    {
        int[,] Map = new int[inputx, inputy];

        for (int x = 0; x < inputx; x++)
        {
            for (int y = 0; y < inputy; y++)
            {
                Map[inputx - 1 - x, inputy - 1 - y] = Stage[x, y];
            }
        }

        return Map;
    }
    public int[] ReturnPos(int x, int y)
    {
        int[] ans = new int[2];

        ans[0] = inputx - 1 - x;
        ans[1] = inputy - 1 - y;

        return ans;
    }
}