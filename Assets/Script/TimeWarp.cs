using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarp : MonoBehaviour
{
    public List<TimeList> Timelist = new List<TimeList>();
    public bool Return = false;
    private bool Record = true;

    private void Start()
    {
        TimeList Poslist = new TimeList();
        Timelist.Add(Poslist);
    }

    void Update()
    {
        if (Record && !Return)
        {
            StartCoroutine("Recorder");
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

    IEnumerator Recorder()
    {
        Record = false;
        Timelist[Timelist.Count - 1].Poslist.Add(transform.position);
        yield return new WaitForSeconds(0.5f);
        Record = true;
    }

    public void CntControl()
    {
        TimeList Poslist = new TimeList();
        Timelist.Add(Poslist);
        Timelist[Timelist.Count - 1].Poslist.Add(transform.position);
    }
}

public class TimeLists
{
    public List<Vector3> Poslist = new List<Vector3>();
}
