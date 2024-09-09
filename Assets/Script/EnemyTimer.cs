using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTimer : MonoBehaviour
{
    public float Timer = 0.0f;

    private void Update()
    {
        Timer += Time.deltaTime;
    }
}
