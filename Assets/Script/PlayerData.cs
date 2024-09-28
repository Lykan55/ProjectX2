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
    private Collider2D ReturnPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReturnPos = collision.GetComponent<Collider2D>();

        if (collision.tag == "Area" && !Return)
        {
            PlayerPrePos = PlayerCurrPos;

            PlayerCurrPos = collision.transform.position;

            Movement = true;
        }
    }

    private void Summon()
    {
        Vector3 pos = new Vector3(PlayerPrePos.x, PlayerPrePos.y);
        GameObject boxB = Instantiate(Enemy, pos, Quaternion.identity);
    }

    public void SetData()
    {
        PlayerCurrPos = ReturnPos.transform.position;
        PlayerPrePos = PlayerCurrPos;
    }
}
