using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UnityEngine.UI를 사용하여 Slider를 참조

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject Btn_Funding;
    public GameObject Btn_Donation;
    public GameObject Btn_Tracking;
    public GameObject Page_Donation;
    public GameObject Page_Tracking;
    public GameObject Page_Funding;
    public GameObject Slider_Funding;

    private Slider sliderComponent;

    public int maxCnt = 5;

    private void Awake() {
        instance = this;
        sliderComponent = Slider_Funding.GetComponent<Slider>();
    }

    public void pushFBtn()
    {
        TogglePage(Page_Funding);
    }

    public void pushDBtn()
    {
        TogglePage(Page_Donation);
    }

    public void pushTBtn()
    {
        TogglePage(Page_Tracking);
    }

    private void TogglePage(GameObject page)
    {
        if (!page.activeSelf)
        {
            DisableAllPages();
            PlayerMove.Instance.think();
            page.SetActive(true);
        }
        else
        {
            PlayerMove.Instance.idle();
            page.SetActive(false);
        }
    }

    private void DisableAllPages()
    {   
        Page_Funding.SetActive(false);
        Page_Donation.SetActive(false);
        Page_Tracking.SetActive(false);
    }

    public void fund()
    {
        maxCnt++;
        float newValue = 100f / maxCnt;
        sliderComponent.value = newValue; // 슬라이더 값 설정
    }
}
