using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMaker : MonoBehaviour
{
    public GameObject Enemy;
    public int SummonNumber;

    private List<GameObject> Enemys = new List<GameObject>();

    public void Summon(List<int[]> SummonPos)
    {
        if (SummonNumber > SummonPos.Count)
        {
            SummonNumber = SummonPos.Count;
        }

        for (int i = 0; i < SummonNumber; i++)
        {
            Vector2 Pos = new Vector2(SummonPos[i][0] * 20, SummonPos[i][1] * 20);
            Enemys.Add(Instantiate(Enemy, Pos, Quaternion.identity));
            Enemys[i].transform.parent = transform;
        }
    }

    public List<GameObject> ReturnEnemys()
    {
        return Enemys;
    }
}
