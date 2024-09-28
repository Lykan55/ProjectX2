using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalDirectionItem : MonoBehaviour
{
    public ItemManager itemManager;
    public Transform player;
    public Transform goal;
    public RectTransform arrowUI; // ���UI (Image�Ƃ��Đݒ�)
private bool isArrowActive = false;

public void UseGoalDirectionItem()
{
    if (player != null && goal != null)
    {
        // ����L����
        isArrowActive = true;
        arrowUI.gameObject.SetActive(true);

        // �R���[�`���ň�莞�Ԍ�ɔ�\���ɂ���
        StartCoroutine(HideArrowAfterTime(3f)); // 3�b��ɏ�����
    }
}

void Update()
{
    if (isArrowActive)
    {
        UpdateArrowDirection();
    }
}

void UpdateArrowDirection()
{
    if (player != null && goal != null)
    {
        // �S�[���ւ̕����x�N�g�����v�Z
        Vector3 direction = goal.position - player.position;
        direction.Normalize();

        // �v���C���[���猩���S�[���̕�����2D�X�N���[�����W�ɕϊ�
        Vector2 screenPos = WorldToScreenDirection(direction);

        // ������]�����ăS�[���̕������w���悤�ɂ���
        float angle = Mathf.Atan2(screenPos.x, screenPos.y) * Mathf.Rad2Deg * -1;
        arrowUI.rotation = Quaternion.Euler(0, 0, angle);

        // �����v���C���[�ɒǏ]������
        arrowUI.position = Camera.main.WorldToScreenPoint(player.position);
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

