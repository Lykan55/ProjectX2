using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeOrthoSizeOnKeyPress : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float newOrthoSize = 10f;
    private float originalOrthoSize;

    void Start()
    {
        if (virtualCamera != null)
        {
            // 現在のOrthographic Sizeを保存しておく
            originalOrthoSize = virtualCamera.m_Lens.OrthographicSize;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (virtualCamera != null)
            {
                // Ortho Sizeを変更
                virtualCamera.m_Lens.OrthographicSize = newOrthoSize;

                // 5秒後に元に戻すCoroutineを開始
                StartCoroutine(ResetOrthoSizeAfterDelay(5f));
            }
        }
    }

    IEnumerator ResetOrthoSizeAfterDelay(float delay)
    {
        // 指定した時間だけ待機
        yield return new WaitForSeconds(delay);

        // Ortho Sizeを元に戻す
        virtualCamera.m_Lens.OrthographicSize = originalOrthoSize;
    }
}


