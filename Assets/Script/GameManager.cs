using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        // このシーンでは60FPSに設定
        Application.targetFrameRate = 60;
    }
}
