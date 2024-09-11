using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemManager : MonoBehaviour
{
    // Human1を取得
    public GameObject player;

    public GameObject WallDestroy;

    public float searchPosition = 40f;  // n方向のチャンクの検索数値

    // アイテム使用時の方向を判定 ＋ 疑似的にリアルタイムで監視
    public bool isWPressed = false;
    public bool isAPressed = false;
    public bool isSPressed = false;
    public bool isDPressed = false;

    // 座標+40の位置のオブジェクト親オブジェクトのタグ
    public string targetTagPlusX;
    public string targetTagMinusX;

    // Areaオブジェクト判定
    public GameObject AreaPlusX;
    public GameObject AreaMinusX;

    // Areaオブジェクトのタグを格納
    public string targetAreaTagPlusX;
    public string targetAreaTagMinusX;

    // objectレイヤー
    public LayerMask Objectlayer;
    // Areaレイヤー
    public LayerMask Arealayer;

    // アイテムの複数使用制限用
    public bool ItemCheck = false;

    // 視野角変更に使用する
    public CinemachineVirtualCamera virtualCamera;
    public float newOrthoSize = 10f;
    private float originalOrthoSize;

    // 矢印アイテムに関係
    public GoalDirectionItem goalDirectionItem; // ゴール方向アイテムを管理するスクリプトの参照

    // アイテム選択の乱数に使用
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
        // Jキー、Kキー、Lキーが押されたときにアイテムを使用する
        if (Input.GetKeyDown(KeyCode.J))
        {
            UseItem(0); // 1キーが押された場合
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            UseItem(1); // 2キーが押された場合
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            UseItem(2); // 3キーが押された場合
        }

        // キーが押されたらtrueに設定
        if (Input.GetKeyDown(KeyCode.W)) { isWPressed = true; }
        if (Input.GetKeyDown(KeyCode.A)) { isAPressed = true; }
        if (Input.GetKeyDown(KeyCode.S)) { isSPressed = true; }
        if (Input.GetKeyDown(KeyCode.D)) { isDPressed = true; }

        // キーが離されたらfalseに戻す
        if (Input.GetKeyUp(KeyCode.W)) { isWPressed = false; }
        if (Input.GetKeyUp(KeyCode.A)) { isAPressed = false; }
        if (Input.GetKeyUp(KeyCode.S)) { isSPressed = false; }
        if (Input.GetKeyUp(KeyCode.D)) { isDPressed = false; }

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
        ItemCheck = true; // ここでアイテム使用許可
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
        if (ItemCheck == true)
        {
            ItemCheck = false; // アイテムの使用をロック
            if (slotIndex >= 0 && slotIndex < selectedItems.Count)
            {
                Sprite item = selectedItems[slotIndex];
                string itemName = item.name; // アイテムの名前を取得
                Debug.Log($"{slotIndex}: {itemName}を使用"); // 使用したアイテムをデバッグログに表示

                // アイテムのメソッドをここに追加

                if (itemName == "Lens")
                {
                    StartCoroutine(Lens());
                }
                else if (itemName == "ArrowItem")
                {
                    StartCoroutine(Arrow());
                }
                else if (itemName == "WallDestroy")
                {
                    //WallDestroy();
                }
            }
            else
            {
                Debug.Log("アイテムスロットにアイテムがありません");
            }
        }
        else if (ItemCheck == false)
        {
            Debug.Log("同時に複数のアイテムを使用できません");
        }
    }



    //アイテムの処理
    public IEnumerator Lens()// 視野角アイテム処理
    {
        if (virtualCamera != null)
        {
            // Ortho Sizeを変更
            virtualCamera.m_Lens.OrthographicSize = newOrthoSize;

            // ５秒待機
            yield return new WaitForSeconds(5f);

            // Ortho Sizeを元に戻す
            virtualCamera.m_Lens.OrthographicSize = originalOrthoSize;
        }
        ItemCheck = true;
    }



    public IEnumerator Arrow()// 矢印アイテム処理
    {
        // GoalDirectionItemスクリプトを参照
        goalDirectionItem.UseGoalDirectionItem(); // ゴールの方向を表示するメソッドを呼び出す
        // ３秒待機
        yield return new WaitForSeconds(3f);
        ItemCheck = true;
    }

    








    /*
    public void WallDestroy() // 壁破壊アイテム処理
    {
        FindObjectPosition();// 周囲のオブジェクトの座標を取得

        //Human1が現在接触しているタグをもとに判定
        if (player.GetComponent<Human1>().PlayerCollision.gameObject.tag == "wall")// まずは壁に接触してる時だけに限定
        {
            if (AreaPlusX != null && targetAreaTagPlusX == "Area")
            {
                if (targetTagPlusX != "wall" && isDPressed)
                {
                    player.GetComponent<Human1>().PlayerCollision.transform.parent.gameObject.SetActive(false);
                    Debug.Log("右の壁を破壊");
                }
            }
            if (AreaMinusX != null|| targetTagPlusX == null && targetAreaTagMinusX == "Area")
            {
                if (targetTagMinusX != "wall" && isAPressed)
                {
                    player.GetComponent<Human1>().PlayerCollision.transform.parent.gameObject.SetActive(false);
                    Debug.Log("左の壁を破壊");
                }
            }
            else 
            {
                Debug.Log("ここを破壊することはできません");
            }
        }
        ItemCheck = true;
    }
    */


    /*-----------------------------------------------------------------------------------------------------------------------------------------------*/


    // ２チャンク先にあるオブジェクトのタグを取得するメソッド
    public void FindObjectPosition()
    {
        // 現在のチャンク位置からx座標を +40 した場所
        Vector2 targetPositionPlusX = new Vector2(player.GetComponent<PlayerData>().PlayerCurrPos.x + searchPosition, player.GetComponent<PlayerData>().PlayerCurrPos.y + 9.5f);

        // 現在のチャンク位置からx座標を -40 した場所
        Vector2 targetPositionMinusX = new Vector2(player.GetComponent<PlayerData>().PlayerCurrPos.x - searchPosition, player.GetComponent<PlayerData>().PlayerCurrPos.y + 9.5f);

// ２チャンク先のAreaレイヤーの中心の座標

        // 現在のチャンク位置からx座標を +40 した場所
        Vector2 targetAreaPositionPlusX = new Vector2(player.GetComponent<PlayerData>().PlayerCurrPos.x + searchPosition, player.GetComponent<PlayerData>().PlayerCurrPos.y);

        // 現在のチャンク位置からx座標を -40 した場所
        Vector2 targetAreaPositionMinusX = new Vector2(player.GetComponent<PlayerData>().PlayerCurrPos.x - searchPosition, player.GetComponent<PlayerData>().PlayerCurrPos.y);

// OverlapPointを使って、２チャンク先にあるオブジェクトを取得

        // Objectレイヤーの２チャンク先にあるオブジェクトを取得
        Collider2D targetObjectPlusX = Physics2D.OverlapPoint(targetPositionPlusX, Objectlayer);    // Xが +40
        Collider2D targetObjectMinusX = Physics2D.OverlapPoint(targetPositionMinusX, Objectlayer);  // Xが -40

        // Areaレイヤーの２チャンク先にあるオブジェクトを取得
        Collider2D targetAreaPlusX = Physics2D.OverlapPoint(targetAreaPositionPlusX, Arealayer);    // Xが +40
        Collider2D targetAreaMinusX = Physics2D.OverlapPoint(targetAreaPositionMinusX, Arealayer);  // Xが -40

// ２チャンク先のAreaレイヤーのオブジェクト
        if (targetAreaPlusX != null && isDPressed)
        {
            AreaPlusX = targetAreaPlusX.gameObject;
        }
        else if (targetAreaMinusX != null && isAPressed)
        {
            AreaMinusX = targetAreaMinusX.gameObject;
        }
        

// ２チャンク先のAreaレイヤーのタグを取得
        if (targetAreaPlusX != null && isDPressed)
        {
            targetAreaTagPlusX = targetAreaPlusX.gameObject.tag;
        }
        else if (targetAreaMinusX != null && isAPressed)
        {
            targetAreaTagMinusX = targetAreaMinusX.gameObject.tag;
        }

// ２チャンク先のobjectレイヤーの上のタグを取得
        if (targetObjectPlusX != null && targetObjectPlusX.transform.parent != null && isDPressed)
        {
            targetTagPlusX = targetObjectPlusX.gameObject.tag;
        }

        else if (targetObjectMinusX != null && targetObjectMinusX.transform.parent != null && isAPressed)
        {
            targetTagMinusX = targetObjectMinusX.gameObject.tag;
        }
    }
}
