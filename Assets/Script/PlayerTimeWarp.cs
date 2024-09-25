using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerTimeWarp : MonoBehaviour
{
    public List<TimeList> Timelist = new List<TimeList>();
    public bool Return = false;

    private void Start()
    {
        TimeList Poslist = new TimeList();
        Timelist.Add(Poslist);
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
            Timelist[Timelist.Count - 1].Poslist.Add(transform.position);
            CntControl();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" && !Return)
        {
            Timelist[Timelist.Count - 1].Poslist.Add(transform.position);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" && !Return)
        {
            Timelist[Timelist.Count - 1].Poslist.Add(transform.position);
        }
    }



    public void CntControl()
    {
        TimeList Poslist = new TimeList();
        Timelist.Add(Poslist);
        Timelist[Timelist.Count - 1].Poslist.Add(transform.position);
    }
    private void InputList()
    {
        Timelist[Timelist.Count - 1].Poslist.Add(transform.position);
    }

}

public class TimeList
{
    public List<Vector3> Poslist = new List<Vector3>();
}