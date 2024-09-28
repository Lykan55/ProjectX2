using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UpgradeScene : MonoBehaviour
{
    public void EXIT()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PLAY()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
