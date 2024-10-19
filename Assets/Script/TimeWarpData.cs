using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarpData : MonoBehaviour
{
    public List<Vector2> PosList = new List<Vector2>();
    public bool Return = false;

    private void FixedUpdate()
    {
        if (!Return)
        {
            PosList.Add(transform.position);
        }
    }
}
