using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeInOut : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public bool fadeIn = false;
    public bool fadeOut = false;
    bool clear = false;

    public float timeToFade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn == true)
        {   
            print("캔버스그룹 알파" + canvasGroup.alpha);
            if(canvasGroup.alpha < 1)
            {
                
                canvasGroup.alpha += timeToFade * Time.deltaTime;
                if(canvasGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut == true)
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= timeToFade * Time.deltaTime;
                if (canvasGroup.alpha == 0.5f)
                {
                    fadeOut = false;
                }
            }
        }

        if(clear)
        {
            while(canvasGroup.alpha > 0){
                 canvasGroup.alpha -= timeToFade * Time.deltaTime;
            }
        }

    }

    public void FadeIn()
    {
        print("fadein실행");
        fadeIn = true;
    }

    public void FadeOut()
    {
        fadeOut = true;
    }

    public void Clear()
    {
        clear = true;
    }
}
