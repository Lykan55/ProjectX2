using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaaaaaa : MonoBehaviour
{
    public GameObject aaa;

    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            GameObject aaaaaa = Instantiate(aaa);
            Destroy(this.gameObject);
        }
    }
}
