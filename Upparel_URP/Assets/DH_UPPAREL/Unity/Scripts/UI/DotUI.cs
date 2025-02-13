using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotUI : MonoBehaviour
{
    [SerializeField]
    RectTransform copyContent;
    [SerializeField]
    RectTransform content;
    [SerializeField]
    GameObject dot;

    [SerializeField]
    GameObject[] dots;

    [SerializeField]
    SliderBar bar;
    // Start is called before the first frame update
    void Start()
    {
        int count = copyContent.transform.childCount;
        dots = new GameObject[count];
        //Debug.Log(count);
        for(int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(dot);
            go.transform.SetParent(content);
            dots[i] = go;
        }
        bar.onChanged = (index) => { Action(dots[index]); };
        for (int i = 0; i < dots.Length; i++)
        {
            //dots[i].GetComponent<Image>().color = Color.black;
        }
        Action(dots[0]);
    }

    
    public void Action(GameObject go)
    {
        for(int i = 0; i < dots.Length; i++)
        {
            dots[i].GetComponent<Image>().color = Color.white;
        }
        Image image = go.GetComponent<Image>();
        image.color = Color.black;
    }
}
