using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject stageMaker;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy" && stageMaker.GetComponent<stage>().GemNumber == 0)
        {
            SceneManager.LoadScene("GameClear");
        }
        else
        {
            Debug.Log("ƒWƒFƒ€‚ª‚Ü‚¾Žc‚Á‚Ä‚¢‚Ü‚·");
        }
    }
}
