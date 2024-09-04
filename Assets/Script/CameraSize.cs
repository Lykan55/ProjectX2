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
            // ���݂�Orthographic Size��ۑ����Ă���
            originalOrthoSize = virtualCamera.m_Lens.OrthographicSize;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (virtualCamera != null)
            {
                // Ortho Size��ύX
                virtualCamera.m_Lens.OrthographicSize = newOrthoSize;

                // 5�b��Ɍ��ɖ߂�Coroutine���J�n
                StartCoroutine(ResetOrthoSizeAfterDelay(5f));
            }
        }
    }

    IEnumerator ResetOrthoSizeAfterDelay(float delay)
    {
        // �w�肵�����Ԃ����ҋ@
        yield return new WaitForSeconds(delay);

        // Ortho Size�����ɖ߂�
        virtualCamera.m_Lens.OrthographicSize = originalOrthoSize;
    }
}


