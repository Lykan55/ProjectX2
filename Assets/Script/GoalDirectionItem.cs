using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDirectionItem : MonoBehaviour
{
    public Transform player;
    public Transform goal;
    public RectTransform arrowUI; // ���UI (Image�Ƃ��Đݒ�)

    public void UseGoalDirectionItem()
    {
        if (player != null && goal != null)
        {
            // �S�[���ւ̕����x�N�g�����v�Z
            Vector3 direction = goal.position - player.position;
            direction.Normalize();

            // �v���C���[���猩���S�[���̕�����2D�X�N���[�����W�ɕϊ�
            Vector2 screenPos = WorldToScreenDirection(direction);

            // ������]�����ăS�[���̕������w���悤�ɂ���
            float angle = Mathf.Atan2(screenPos.y, screenPos.x) * Mathf.Rad2Deg;
            arrowUI.rotation = Quaternion.Euler(0, 0, angle);

            // �����v���C���[�ɒǏ]������
            arrowUI.position = Camera.main.WorldToScreenPoint(player.position);

            // UI��L���ɂ��ĕ�����\��
            arrowUI.gameObject.SetActive(true);

            // ���b���UI���\���ɂ���R���[�`�����J�n
            StartCoroutine(HideArrowAfterTime(3f)); // 3�b��ɏ�����
        }
    }

    // ������莞�Ԍ�ɔ�\���ɂ���R���[�`��
    IEnumerator HideArrowAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        arrowUI.gameObject.SetActive(false); // ���UI���\���ɂ���
    }

    // 3D��Ԃ̕����x�N�g�����X�N���[�����W�n�ɕϊ�
    Vector2 WorldToScreenDirection(Vector3 worldDirection)
    {
        Vector3 screenDirection = Camera.main.WorldToScreenPoint(player.position + worldDirection);
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(player.position);
        return (screenDirection - playerScreenPos).normalized;
    }
}

