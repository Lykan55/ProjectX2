using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.Collections;

public class ItemManager : MonoBehaviour
{
    //視野角変更に使用する
    public CinemachineVirtualCamera virtualCamera;
    public float newOrthoSize = 10f;
    private float originalOrthoSize;


    public List<Sprite> itemImages; // アイテム画像のリストをInspectorで設定
    public Image[] itemSlots; // アイテムを表示するUI Imageオブジェクトの配列

    private List<Sprite> selectedItems = new List<Sprite>();

    void Start()
    {
        if (virtualCamera != null)
        {
            // 現在のOrthographic Sizeを保存しておく
            originalOrthoSize = virtualCamera.m_Lens.OrthographicSize;
        }
        // アイテムをランダムに選び、重複しないように表示する
        SelectItems();
        DisplayItems();
    }

    void Update()
    {
        // 1キー、2キー、3キーが押されたときにアイテムを使用する
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseItem(0); // 1キーが押された場合
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseItem(1); // 2キーが押された場合
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseItem(2); // 3キーが押された場合
        }
    }

    //視野角変更処理
    IEnumerator ResetOrthoSizeAfterDelay(float delay)
    {
        // 指定した時間だけ待機
        yield return new WaitForSeconds(delay);

        // Ortho Sizeを元に戻す
        virtualCamera.m_Lens.OrthographicSize = originalOrthoSize;
    }

    void SelectItems()
    {
        HashSet<int> selectedIndices = new HashSet<int>();

        // 3つのアイテムを選択
        while (selectedIndices.Count < 3)
        {
            int index = Random.Range(0, itemImages.Count);
            selectedIndices.Add(index);
        }

        // 選ばれたアイテムをリストに追加
        selectedItems.Clear();
        foreach (int index in selectedIndices)
        {
            selectedItems.Add(itemImages[index]);
        }
    }

    void DisplayItems()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < selectedItems.Count)
            {
                itemSlots[i].sprite = selectedItems[i];
                itemSlots[i].enabled = true; // アイテムがある場合は表示
            }
            else
            {
                itemSlots[i].enabled = false; // アイテムがない場合は非表示
            }
        }
    }

    void UseItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < selectedItems.Count)
        {
            Sprite item = selectedItems[slotIndex];
            string itemName = item.name; // アイテムの名前を取得

            Debug.Log($"Using item in slot {slotIndex}: {itemName}"); // デバッグログを追加

            // アイテムの使用処理をここに追加
            if (itemName == "Lens")
            {
                if (virtualCamera != null)
                {
                    // Ortho Sizeを変更
                    virtualCamera.m_Lens.OrthographicSize = newOrthoSize;

                    // 5秒後に元に戻すCoroutineを開始
                    StartCoroutine(ResetOrthoSizeAfterDelay(5f));
                }
            }
            // ここでアイテムの具体的な使用処理を実装
        }
        else
        {
            Debug.Log("No item in this slot.");
        }
    }
}
