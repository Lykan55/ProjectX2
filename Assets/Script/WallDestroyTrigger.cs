using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallDestroyTrigger : MonoBehaviour
{
    public bool destroy = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            collision.transform.parent.gameObject.SetActive(false);
            Debug.Log("�v���n�u���Ăяo����āA�N���[������ĕǂ��폜������");
        }
        else
        {
            Debug.Log("�����͔j��ł��܂���");
        }
        Destroy(gameObject);
    }
}
