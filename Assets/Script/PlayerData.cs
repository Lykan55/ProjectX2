using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerData : MonoBehaviour
{
    public GameObject Enemy;
    public Vector3 PlayerCurrPos = new Vector2();
    public Vector3 PlayerPrePos = new Vector2();
    public bool Movement = false;
    public bool Return = false;
    private List<Vector3> PosList = new List<Vector3>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Area" && !Return)
        {
            PlayerPrePos = PlayerCurrPos;

            if (PosList.Count == 1)
            {
                Invoke(nameof(Summon), 0.8f);
            }

            PlayerCurrPos = collision.transform.position;

            PosList.Add(PlayerCurrPos);

            Movement = true;
        }
    }

    private void Summon()
    {
        Vector3 pos = new Vector3(PlayerPrePos.x, PlayerPrePos.y);
        GameObject boxB = Instantiate(Enemy, pos, Quaternion.identity);
    }
}
