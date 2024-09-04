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
    private bool Summon1 = true;
    private bool Summon2 = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Area")
        {
            PlayerPrePos = PlayerCurrPos;
            if (Summon1 == true && Summon2 == true)
            {
                Invoke(nameof(Summon), 0.5f);
            }
            Summon2 = true;

            PlayerCurrPos = collision.transform.position;

            Movement = true;
        }
    }

    private void Summon()
    {
        Vector2 pos = new Vector2(PlayerPrePos.x, PlayerPrePos.y);
        GameObject boxB = Instantiate(Enemy, pos, Quaternion.identity);
        Summon1 = false;
    }
}
