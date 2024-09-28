using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("ê⁄êG");
        if(collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);
        }
    }
}
