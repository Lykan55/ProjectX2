using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemCT : MonoBehaviour
{
    public GameObject item;
    public GameObject left;
    public GameObject right;
    public GameObject mid;
    public Sprite red;
    public Sprite green;

    void Update()
    {
        if (item.GetComponent<ItemManager>().ItemCheck)
        {
            left.GetComponent<Image>().sprite = green;
            right.GetComponent<Image>().sprite = green;
            mid.GetComponent<Image>().sprite = green;
        }
        else
        {
            left.GetComponent<Image>().sprite = red;
            right.GetComponent<Image>().sprite = red;
            mid.GetComponent<Image>().sprite = red;
        }
    }
}
