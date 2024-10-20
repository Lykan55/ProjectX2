using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallDestroyTrigger : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject,0.5f);
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
