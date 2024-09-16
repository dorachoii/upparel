using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UnityEngine.UI를 사용하여 Slider를 참조

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject Canvas_shopUI;
    public GameObject Canvas_center;
    public GameObject Canvas_challenge;

    private Slider sliderComponent;

    public int maxCnt = 5;

    private void Awake() {
        instance = this;
        
    }

    // public void pushFBtn()
    // {
    //     TogglePage(Canvas_challenge);
    // }

    // public void pushDBtn()
    // {
    //     TogglePage(Canvas_shopUI);
    // }

    // public void pushTBtn()
    // {
    //     TogglePage(Canvas_center);
    // }

    // private void TogglePage(GameObject page)
    // {
    //     if (!page.activeSelf)
    //     {
    //         DisableAllPages();
    //         PlayerMove.Instance.think();
    //         page.SetActive(true);
    //     }
    //     else
    //     {
    //         PlayerMove.Instance.idle();
    //         page.SetActive(false);
    //     }
    // }

    // private void DisableAllPages()
    // {   
    //     Canvas_challenge.SetActive(false);
    //     Canvas_shopUI.SetActive(false);
    //     Canvas_center.SetActive(false);
    // }

    // public void fund()
    // {
    //     maxCnt++;
    //     float newValue = 100f / maxCnt;
    //     sliderComponent.value = newValue; // 슬라이더 값 설정
    // }
}
