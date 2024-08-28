using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage : MonoBehaviour
{
    static int x;
    static int y;
    static int[,] Stage;
    static int[] data = { -1, -1, -1, -1 };
    static int[] rist = new int[1];
    static int cnt = 0;
    static int go = -1;
    static int end = 0;

    public int width;
    public int height;
    static int inputx;
    static int inputy;

    public GameObject stageblockA;
    public GameObject stageblockB;

    void Start()
    {
        inputx = inputnumber(width);
        inputy = inputnumber(height);

        Stage = new int[inputx, inputy];


        for (y = 0; y < inputy; y++)
        {
            for (x = 0; x < inputx; x++)
            {
                Stage[x, y] = 0;
            }
        }


        firstpoint();


        while (end == 0)
        {
            go = -1;
            for (int a = 0; a < 4; a++)
            {
                data[a] = -1;
            }

            search();

            if (data[0] == -1 && data[1] == -1 && data[2] == -1 && data[3] == -1)
            {
                nothing();
            }
            else
            {
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

        for (y = 0; y < inputy; y++)
        {
            for (x = 0; x < inputx; x++)
            {
                if (Stage[x, y] == 0)
                {
                    Vector2 pos = new Vector2((inputx - 1 - x) * 20, (inputy - 1 - y) * 20);
                    GameObject boxA = Instantiate(stageblockA, pos, Quaternion.identity);
                    boxA.transform.parent = transform;
                }
                else if (Stage[x, y] == 1)
                {

                }
                else
                {
                    Vector2 pos = new Vector2((inputx - 1 - x) * 20, (inputy - 1 - y) * 20);
                    GameObject boxB = Instantiate(stageblockB, pos, Quaternion.identity);
                    boxB.transform.parent = transform;
                }
            }
        }
    }





    static int inputnumber(int input)
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

    static void firstpoint()
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
    }

    static void search()
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

    static void nothing()
    {
        if (cnt == 0)
        {
            end++;
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

    static void going()
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
}
