using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TimeWarp : MonoBehaviour
{
    public TimeList[] TimeList = new TimeList[4];
    private int ListCnt = 0;
    public bool Return = false;

    private void Start()
    {
        for (int i = 0; i < TimeList.Length; i++)
        {
            TimeList[i] = new TimeList();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Return)
        {
            Invoke("InputList", 0.3f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Area" && !Return)
        {
            CntControl();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" && !Return)
        {
            TimeList[ListCnt].Poslist.Add(transform.position);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" && !Return)
        {
            TimeList[ListCnt].Poslist.Add(transform.position);
        }
    }



    public void CntControl()
    {
        ListCnt++;

        if (ListCnt >= TimeList.Length)
        {
            ListCnt = 0;
            while (ListCnt != TimeList.Length - 1)
            {
                TimeList[ListCnt] = TimeList[ListCnt + 1];
                ListCnt++;
            }
            TimeList[ListCnt] = new TimeList();
        }

        TimeList[ListCnt].Poslist.Add(transform.position);
    }
    private void InputList()
    {
        TimeList[ListCnt].Poslist.Add(transform.position);
    }

}

public class TimeList
{
    public List<Vector3> Poslist = new List<Vector3>();
}