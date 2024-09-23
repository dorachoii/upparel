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


    // rebud: 0, recode: 1, cueclyp : 2, overlab : 3
    public GameObject[] Canvas_shopUI_Logo;
    public GameObject[] Canvas_shopUI_brandInfo;
    public GameObject[] Canvas_shopUI_Archive;
    public GameObject[] Canvas_shopUI_Merchandise;
    public GameObject[] Canvas_shopUI_details;



    public int maxCnt = 5;

    private void Awake() {
        instance = this;
        Debug.Log("UIManager실행");

        if (CharacterSelection.instance != null)
        {
            CharacterSelection.instance.InstantiateSelectedCharacterInMainScene();
        }
        else
        {
            Debug.LogWarning("CharacterSelection 싱글톤 인스턴스에 접근할 수 없습니다.");
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CharacterSelection.instance.InstantiateSelectedCharacterInMainScene();
        }
    }
}
