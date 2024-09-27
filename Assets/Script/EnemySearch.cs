using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{
    private bool WallFlag = false;
    private bool EndFlag = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "wall")
        {
            WallFlag = true;
            Debug.Log("AAAAAAAAAAAAAAAAA");
        }
    }
    //oooooooooooooooooooooooooooooooooooooooooooooooooooooooo
    public Vector3 TargetSearch(int front)
    {
        while (!WallFlag && !EndFlag)
        {
            switch (front)
            {
                case 0:
                    transform.Translate(0, 20, 0, Space.World);
                    break;
                case 3:
                    transform.Translate(20, 0, 0, Space.World);
                    break;
                case 6:
                    transform.Translate(0, -20, 0, Space.World);
                    break;
                case 9:
                    transform.Translate(-20, 0, 0, Space.World);
                    break;
            }

            Check(front);
        }

        if (WallFlag)
        {
            switch (front)
            {
                case 0:
                    transform.Translate(0, -20, 0, Space.World);
                    break;
                case 3:
                    transform.Translate(-20, 0, 0, Space.World);
                    break;
                case 6:
                    transform.Translate(0, 20, 0, Space.World);
                    break;
                case 9:
                    transform.Translate(20, 0, 0, Space.World);
                    break;
            }
        }

        return transform.position;
    }
    private void Check(int front)
    {
        if (!WallFlag)
        {
            if (front == 0 || front == 6)
            {
                transform.Translate(20, 0, 0, Space.World);
                if (WallFlag)
                {
                    WallFlag = false;
                }
                else
                {
                    EndFlag = true;
                }

                transform.Translate(-40, 0, 0, Space.World);
                if (WallFlag)
                {
                    WallFlag = false;
                }
                else
                {
                    EndFlag = true;
                }

                transform.Translate(20, 0, 0, Space.World);
            }
            else
            {
                transform.Translate(0, 20, 0, Space.World);
                if (WallFlag)
                {
                    WallFlag = false;
                }
                else
                {
                    EndFlag = true;
                }

                transform.Translate(0, -40, 0, Space.World);
                if (WallFlag)
                {
                    WallFlag = false;
                }
                else
                {
                    EndFlag = true;
                }

                transform.Translate(0, 20, 0, Space.World);
            }
        }
    }
    //ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
    public int FrontSearch()
    {
        int[] Data = { 0, 0, 0, 0 };

        transform.Translate(0, 20, 0, Space.World);
        if (!WallFlag)
        {
            Data[0] = 1;
        }
        WallFlag = false;

        transform.Translate(20, -20, 0, Space.World);
        if (!WallFlag)
        {
            Data[1] = 1;
        }
        WallFlag = false;

        transform.Translate(-20, -20, 0, Space.World);
        if (!WallFlag)
        {
            Data[2] = 1;
        }
        WallFlag = false;

        transform.Translate(-20, 20, 0, Space.World);
        if (!WallFlag)
        {
            Data[3] = 1;
        }
        WallFlag = false;

        transform.Translate(20, 0, 0, Space.World);

        while (true)
        {
            int n = Random.Range(0, 4);
            if (Data[n] == 1)
            {
                return n * 3;
            }
        }
    }
}
