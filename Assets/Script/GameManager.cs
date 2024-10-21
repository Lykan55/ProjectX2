using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        // ‚±‚ÌƒV[ƒ“‚Å‚Í60FPS‚Éİ’è
        Application.targetFrameRate = 60;
    }
}
