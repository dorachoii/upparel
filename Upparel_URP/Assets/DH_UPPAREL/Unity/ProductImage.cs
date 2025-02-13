using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProductImage : MonoBehaviour,IPointerClickHandler
{
    public static Action<ProductImage> onclick;
    public Texture2D texture;
    public void OnPointerClick(PointerEventData eventData)
    {
        onclick?.Invoke(this);
        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
