using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalDirectionItem : MonoBehaviour
{
    public ItemManager itemManager;
    public Transform player;
    public Transform goal;
    public RectTransform arrowUI; // 矢印UI (Imageとして設定)
private bool isArrowActive = false;

public void UseGoalDirectionItem()
{
    if (player != null && goal != null)
    {
        // 矢印を有効化
        isArrowActive = true;
        arrowUI.gameObject.SetActive(true);

        // コルーチンで一定時間後に非表示にする
        StartCoroutine(HideArrowAfterTime(3f)); // 3秒後に消える
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
        // ゴールへの方向ベクトルを計算
        Vector3 direction = goal.position - player.position;
        direction.Normalize();

        // プレイヤーから見たゴールの方向を2Dスクリーン座標に変換
        Vector2 screenPos = WorldToScreenDirection(direction);

        // 矢印を回転させてゴールの方向を指すようにする
        float angle = Mathf.Atan2(screenPos.x, screenPos.y) * Mathf.Rad2Deg * -1;
        arrowUI.rotation = Quaternion.Euler(0, 0, angle);

        // 矢印をプレイヤーに追従させる
        arrowUI.position = Camera.main.WorldToScreenPoint(player.position);
    }
}


    // 矢印を一定時間後に非表示にするコルーチン
    IEnumerator HideArrowAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        arrowUI.gameObject.SetActive(false); // 矢印UIを非表示にする
    }

    // 3D空間の方向ベクトルをスクリーン座標系に変換
    Vector2 WorldToScreenDirection(Vector3 worldDirection)
    {
        Vector3 screenDirection = Camera.main.WorldToScreenPoint(player.position + worldDirection);
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(player.position);
        return (screenDirection - playerScreenPos).normalized;
    }
}

