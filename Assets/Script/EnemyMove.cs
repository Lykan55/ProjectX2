using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMove : MonoBehaviour
{
    GameObject Player;
    public GameObject Search;

    Vector3 TargetPos;
    private int Front;

    public List<Vector3> PosList = new List<Vector3>();
    public bool Return = false;

    void Start()
    {
        Player = GameObject.Find("Human 1");
        SetData();
        PosList.Add(transform.position);
    }

    void Update()
    {
        if (TargetPos == transform.position)
        {
            SetData();
        }

        transform.position = Vector3.MoveTowards(transform.position, TargetPos, 0.1f);
    }

    void SetData()
    {
        GameObject Searcher = Instantiate(Search, transform.position, Quaternion.identity);
        Front = Searcher.GetComponent<EnemySearch>().FrontSearch();
        TargetPos = Searcher.GetComponent<EnemySearch>().TargetSearch(Front);
        Debug.Log($"{Front}");
        Debug.Log($"{TargetPos}");
        Destroy(Searcher);
    }
}
