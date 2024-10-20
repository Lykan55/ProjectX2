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
            Debug.Log("ƒNƒ[ƒ“‚³‚ê‚Ä•Ç‚ğíœ‚µ‚½‚æ");
        }
        else
        {
            Debug.Log("¶¬ŒãB‚±‚±‚Í”j‰ó‚Å‚«‚Ü‚¹‚ñ");
        }
    }
}
