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

    public GameObject wallDestroy;

    public GameObject Return;

    public float searchPosition = 40f;  // n方向のチャンクの検索数値

    // アイテム使用時の方向を判定 ＋ 疑似的にリアルタイムで監視
    public bool isWPressed = false;
    public bool isAPressed = false;
    public bool isSPressed = false;
    public bool isDPressed = false;

    // 座標+40の位置のオブジェクトのタグ
    public string targetTagPlusX;
    public string targetTagMinusX;
    public string targetTagMinusY;

    // Areaレイヤーのタグを格納
    public string targetAreaTagPlusX;
    public string targetAreaTagMinusX;
    public string targetAreaTagMinusY;

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
        // ここでアイテム使用許可
        ItemCheck = true; 
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
                    WallDestroy();
                }
                else if(itemName == "Return")
                {
                    Vector2 pos = new Vector2(0,0);
                    Instantiate(Return, pos, Quaternion.identity);
                }
            }
            else
            {
                Debug.Log("アイテムスロットにアイテムがありません");
            }
            // スキルCT開始
            StartCoroutine(CT());            
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
    }



    public IEnumerator Arrow()// 矢印アイテム処理
    {
        // GoalDirectionItemスクリプトを参照
        goalDirectionItem.UseGoalDirectionItem(); // ゴールの方向を表示するメソッドを呼び出す
        // ３秒待機
        yield return new WaitForSeconds(3f);
    }


    public void  WallDestroy()// 壁破壊アイテム処理
    {
        FindArea();
        FindWall();
        if (isDPressed && targetAreaTagPlusX =="Area" && targetTagPlusX != "wall")
        {
            Vector2 pos = new Vector2(player.GetComponent<PlayerData>().PlayerCurrPos.x + 20f, player.GetComponent<PlayerData>().PlayerCurrPos.y);
            Instantiate(wallDestroy, pos, Quaternion.identity);
        }
        else if (isAPressed && targetAreaTagMinusX =="Area" && targetTagMinusX != "wall")
        {
            Vector2 pos = new Vector2(player.GetComponent<PlayerData>().PlayerCurrPos.x - 20f, player.GetComponent<PlayerData>().PlayerCurrPos.y);
            Instantiate(wallDestroy, pos, Quaternion.identity);
        }
        else if (isSPressed && targetAreaTagMinusY == "Area" && targetTagMinusY != "wall")
        {
            Vector2 pos = new Vector2(player.GetComponent<PlayerData>().PlayerCurrPos.x, player.GetComponent<PlayerData>().PlayerCurrPos.y - 20f);
            Instantiate(wallDestroy, pos, Quaternion.identity);
        }
        else
        {
            Debug.Log("ここは破壊できません");
        }
        // 一応タグを初期化
        targetAreaTagPlusX = null;
        targetAreaTagMinusX = null;
        targetAreaTagMinusY = null;
        targetTagPlusX = null;
        targetTagMinusX = null;
        targetTagMinusY = null;
    }

    /*-----------------------------------------------------------------------------------------------------------------------------------------------*/

    public void FindArea()
    {
        // ２チャンク先のAreaレイヤーの中心の座標

        // 現在のチャンク位置からx座標を +40 した場所
        Vector2 targetAreaPositionPlusX = new Vector2(player.GetComponent<PlayerData>().PlayerCurrPos.x + searchPosition, player.GetComponent<PlayerData>().PlayerCurrPos.y);

        // 現在のチャンク位置からx座標を -40 した場所
        Vector2 targetAreaPositionMinusX = new Vector2(player.GetComponent<PlayerData>().PlayerCurrPos.x - searchPosition, player.GetComponent<PlayerData>().PlayerCurrPos.y);

        // 現在のチャンク位置からy座標を -40 した場所
        Vector2 targetAreaPositionMinusY = new Vector2(player.GetComponent<PlayerData>().PlayerCurrPos.x, player.GetComponent<PlayerData>().PlayerCurrPos.y - searchPosition);

        // Areaレイヤーの２チャンク先にあるオブジェクトを取得
        Collider2D targetAreaPlusX = Physics2D.OverlapPoint(targetAreaPositionPlusX, Arealayer);    // Xが +40
        Collider2D targetAreaMinusX = Physics2D.OverlapPoint(targetAreaPositionMinusX, Arealayer);  // Xが -40
        Collider2D targetAreaMinusY = Physics2D.OverlapPoint(targetAreaPositionMinusY, Arealayer);  // Xが -40


        // ２チャンク先のAreaレイヤーのタグを取得
        if (targetAreaPlusX != null)
        {
            targetAreaTagPlusX = targetAreaPlusX.gameObject.tag;
        }
        if (targetAreaMinusX != null)
        {
            targetAreaTagMinusX = targetAreaMinusX.gameObject.tag;
        }
        if (targetAreaMinusY != null)
        {
            targetAreaTagMinusY = targetAreaMinusY.gameObject.tag;
        }
    }

    public void FindWall()
    {
        // 現在のチャンク位置からx座標を +40 した場所
        Vector2 targetPositionPlusX = new Vector2(player.GetComponent<PlayerData>().PlayerCurrPos.x + searchPosition, player.GetComponent<PlayerData>().PlayerCurrPos.y);

        // 現在のチャンク位置からx座標を -40 した場所
        Vector2 targetPositionMinusX = new Vector2(player.GetComponent<PlayerData>().PlayerCurrPos.x - searchPosition, player.GetComponent<PlayerData>().PlayerCurrPos.y);

        // 現在のチャンク位置からy座標を -40 した場所
        Vector2 targetPositionMinusY = new Vector2(player.GetComponent<PlayerData>().PlayerCurrPos.x, player.GetComponent<PlayerData>().PlayerCurrPos.y - searchPosition);


        // Objectレイヤーの２チャンク先にあるオブジェクトを取得
        Collider2D targetObjectPlusX = Physics2D.OverlapPoint(targetPositionPlusX, Objectlayer);    // Xが +40
        Collider2D targetObjectMinusX = Physics2D.OverlapPoint(targetPositionMinusX, Objectlayer);  // Xが -40
        Collider2D targetObjectMinusY = Physics2D.OverlapPoint(targetPositionMinusY, Objectlayer);  // Xが -40


        if (targetObjectPlusX != null)
        {
            targetTagPlusX = targetObjectPlusX.gameObject.tag;
        }
        if(targetObjectMinusX != null)
        {
            targetTagMinusX = targetObjectMinusX.gameObject.tag;
        }
        if (targetObjectMinusY != null)
        {
            targetTagMinusY = targetObjectMinusY.gameObject.tag;
        }
    }
    private IEnumerator CT()
    {
        yield return new WaitForSeconds(5f);
        ItemCheck = true;
    }
}
