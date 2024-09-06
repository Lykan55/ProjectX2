using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTimer : MonoBehaviour
{
    public float Timer = 0.0f;
    GameObject Player;

    private void Start()
    {
        Player = GameObject.Find("Human 1");
    }
    private void Update()
    {
        Timer += Time.deltaTime;
    }
}
