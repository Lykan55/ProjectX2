using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public bool isTrigger = false;
    public BoxCollider2D C;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isTrigger)
            {
                offCollider();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        C.enabled = true;
        isTrigger = false;
    }

    public void offCollider()
    {
        C.enabled = false;
    }
}
