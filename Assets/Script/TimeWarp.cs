using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarp : MonoBehaviour
{
    public TimeList[] TimeList = new TimeList[3];
    private int ListCnt = 0;
    public bool Return = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !Return)
        {
            Invoke("InputList", 0.3f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Area" && !Return)
        {
            CntControl();
            TimeList[ListCnt].Poslist.Add(collision.transform.position);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" && !Return)
        {
            TimeList[ListCnt].Poslist.Add(collision.transform.position);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" && !Return)
        {
            TimeList[ListCnt].Poslist.Add(collision.transform.position);
        }
    }



    private void CntControl()
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
            TimeList[ListCnt] = null;
        }
    }
    private void InputList()
    {
        TimeList[ListCnt].Poslist.Add(transform.position);
    }

}

public class TimeList
{
    public List<Vector3> Poslist;
}