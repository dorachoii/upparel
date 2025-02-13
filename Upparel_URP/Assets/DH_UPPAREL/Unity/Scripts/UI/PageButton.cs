using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageButton : MonoBehaviour
{
    public string container;
    public string address;

    public void Onclick()
    {
        PageManager.Get.ChangePage(container,address);
    }
    // Start is called before the first frame update
    public void NextClick()
    {
        PageManager.Get.PressNext();
    }
    public void PrevClick()
    {
        PageManager.Get.PressPrev(); 
    }
}
