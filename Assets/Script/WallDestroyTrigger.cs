using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallDestroyTrigger : MonoBehaviour
{
    public GameObject itemPanel;
    private void Start()
    {
        itemPanel = GameObject.Find("ItemPanel");
        Destroy(gameObject,0.5f);
    }

    private IEnumerator CheckTrue()
    {
        yield return new WaitForSeconds(0.2f);
        if (itemPanel != null)
        {
            itemPanel.GetComponent<ItemManager>().ItemCheck = true;
        }
        else
        {
            Debug.Log("ItemPanelが見つかりません。");
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            GameObject.Find("enemy 0(Clone)").GetComponent<EnemyMove>().MapNewLoad(collision.transform.position);
            collision.transform.parent.gameObject.SetActive(false);
            Debug.Log("クローンされて壁を削除したよ");
        }
        else
        {
            Debug.Log("生成後。ここは破壊できません");
        }
    }
}
