using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        // ���̃V�[���ł�60FPS�ɐݒ�
        Application.targetFrameRate = 60;
    }
}
