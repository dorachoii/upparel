using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    public string address;
    public bool stackable;
    public void Close()
    {
        gameObject.SetActive(false);
        //depth����?? ���??
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

}
