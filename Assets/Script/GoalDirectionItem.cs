using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDirectionItem : MonoBehaviour
{
    public Transform player;
    public Transform goal;
    public RectTransform arrowUI; // 矢印UI (Imageとして設定)

    public void UseGoalDirectionItem()
    {
        if (player != null && goal != null)
        {
            // ゴールへの方向ベクトルを計算
            Vector3 direction = goal.position - player.position;
            direction.Normalize();

            // プレイヤーから見たゴールの方向を2Dスクリーン座標に変換
            Vector2 screenPos = WorldToScreenDirection(direction);

            // 矢印を回転させてゴールの方向を指すようにする
            float angle = Mathf.Atan2(screenPos.y, screenPos.x) * Mathf.Rad2Deg;
            arrowUI.rotation = Quaternion.Euler(0, 0, angle);

            // 矢印をプレイヤーに追従させる
            arrowUI.position = Camera.main.WorldToScreenPoint(player.position);

            // UIを有効にして方向を表示
            arrowUI.gameObject.SetActive(true);

            // 数秒後にUIを非表示にするコルーチンを開始
            StartCoroutine(HideArrowAfterTime(3f)); // 3秒後に消える
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

