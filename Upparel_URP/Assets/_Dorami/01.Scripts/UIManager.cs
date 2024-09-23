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

    public GameObject popup;


    // rebud: 0, recode: 1, cueclyp : 2, overlab : 3
    public GameObject[] Canvas_shopUI_Logo;
    public GameObject[] Canvas_shopUI_brandInfo;
    public GameObject[] Canvas_shopUI_Archive;
    public GameObject[] Canvas_shopUI_Merchandise;
    public GameObject[] Canvas_shopUI_details;

    string currentShopName = "";

    public int maxCnt = 5;

    public void ActivateShopUI(string shopname)
    {
        currentShopName = shopname;

        if(shopname == "re:bud_collider")
        {
            Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[0].SetActive(true);
            Canvas_shopUI_brandInfo[0].SetActive(true);
            //Canvas_shopUI_details[0].SetActive(true);
            //Canvas_shopUI_Merchandise[0].SetActive(true);
        }
        else if(shopname == "re:code_collder")
        {
            Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[1].SetActive(true);
            Canvas_shopUI_brandInfo[1].SetActive(true);
            //Canvas_shopUI_details[1].SetActive(true);
            //Canvas_shopUI_Merchandise[1].SetActive(true);
        }
        else if(shopname == "cueclyp_collider")
        {
            Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[2].SetActive(true);
            Canvas_shopUI_brandInfo[2].SetActive(true);
            //Canvas_shopUI_details[2].SetActive(true);
            //Canvas_shopUI_Merchandise[2].SetActive(true);
        }
        else if(shopname == "overlab_collider")
        {
            Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[3].SetActive(true);
            Canvas_shopUI_brandInfo[3].SetActive(true);
            //Canvas_shopUI_details[3].SetActive(true);
            //Canvas_shopUI_Merchandise[3].SetActive(true);
        }

        Canvas_shopUI.SetActive(true);
    }

    // x 버튼에 할당
    public void DeactivateShopUI()
    {
        popup.SetActive(false);

        if(currentShopName == "re:bud_collider")
        {
            Canvas_shopUI.SetActive(false);
            Canvas_shopUI_Logo[0].SetActive(false);
            Canvas_shopUI_brandInfo[0].SetActive(false);
            Canvas_shopUI_Archive[0].SetActive(false);
            Canvas_shopUI_details[0].SetActive(false);
            Canvas_shopUI_Merchandise[0].SetActive(false);
        }
        else if(currentShopName == "re:code_collder")
        {
            Canvas_shopUI.SetActive(false);
            Canvas_shopUI_Logo[1].SetActive(false);
            Canvas_shopUI_brandInfo[1].SetActive(false);
            Canvas_shopUI_Archive[1].SetActive(false);
            Canvas_shopUI_details[1].SetActive(false);
            Canvas_shopUI_Merchandise[1].SetActive(false);
        }
        else if(currentShopName == "cueclyp_collider")
        {
            Canvas_shopUI.SetActive(false);
            Canvas_shopUI_Logo[2].SetActive(false);
            Canvas_shopUI_brandInfo[2].SetActive(false);
            Canvas_shopUI_Archive[2].SetActive(false);
            Canvas_shopUI_details[2].SetActive(false);
            Canvas_shopUI_Merchandise[2].SetActive(false);
        }
        else if(currentShopName == "overlab_collider")
        {
            Canvas_shopUI.SetActive(false);
            Canvas_shopUI_Logo[3].SetActive(false);
            Canvas_shopUI_brandInfo[3].SetActive(false);
            Canvas_shopUI_Archive[3].SetActive(false);
            Canvas_shopUI_details[3].SetActive(false);
            Canvas_shopUI_Merchandise[3].SetActive(false);
        }
        
        Canvas_shopUI.SetActive(false);
    }

    public void pushBrandInfo()
    {
        if(currentShopName == "re:bud_collider")
        {
            //Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[0].SetActive(true);
            Canvas_shopUI_brandInfo[0].SetActive(true);
            Canvas_shopUI_details[0].SetActive(false);
            Canvas_shopUI_Archive[0].SetActive(false);
            Canvas_shopUI_Merchandise[0].SetActive(false);
        }
        else if(currentShopName == "re:code_collder")
        {
            //Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[1].SetActive(true);
            Canvas_shopUI_brandInfo[1].SetActive(true);
            Canvas_shopUI_details[1].SetActive(false);
            Canvas_shopUI_Archive[1].SetActive(false);
            Canvas_shopUI_Merchandise[1].SetActive(false);
        }
        else if(currentShopName == "cueclyp_collider")
        {
            //Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[2].SetActive(true);
            Canvas_shopUI_brandInfo[2].SetActive(true);
            Canvas_shopUI_details[2].SetActive(false);
            Canvas_shopUI_Archive[2].SetActive(false);
            Canvas_shopUI_Merchandise[2].SetActive(false);
        }
        else if(currentShopName == "overlab_collider")
        {
            //Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[3].SetActive(true);
            Canvas_shopUI_brandInfo[3].SetActive(true);
            Canvas_shopUI_details[3].SetActive(false);
            Canvas_shopUI_Archive[3].SetActive(false);
            Canvas_shopUI_Merchandise[3].SetActive(false);
        }
    }

    public void pushArchive()
    {
         if(currentShopName == "re:bud_collider")
        {
            Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[0].SetActive(true);
            Canvas_shopUI_brandInfo[0].SetActive(false);
            Canvas_shopUI_details[0].SetActive(false);
            Canvas_shopUI_Archive[0].SetActive(true);
            Canvas_shopUI_Merchandise[0].SetActive(false);
        }
        else if(currentShopName == "re:code_collder")
        {
            Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[1].SetActive(true);
            Canvas_shopUI_brandInfo[1].SetActive(false);
            Canvas_shopUI_details[1].SetActive(true);
            Canvas_shopUI_Archive[1].SetActive(true);
            Canvas_shopUI_Merchandise[1].SetActive(false);
        }
        else if(currentShopName == "cueclyp_collider")
        {
            Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[2].SetActive(true);
            Canvas_shopUI_brandInfo[2].SetActive(false);
            Canvas_shopUI_details[2].SetActive(true);
            Canvas_shopUI_Archive[2].SetActive(true);
            Canvas_shopUI_Merchandise[2].SetActive(false);
        }
        else if(currentShopName == "overlab_collider")
        {
            Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[3].SetActive(true);
            Canvas_shopUI_brandInfo[3].SetActive(false);
            Canvas_shopUI_details[3].SetActive(true);
            Canvas_shopUI_Archive[3].SetActive(true);
            Canvas_shopUI_Merchandise[3].SetActive(false);
        }
    }

    public void pushMerchandise()
    {
        if(currentShopName == "re:bud_collider")
        {
            Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[0].SetActive(true);
            Canvas_shopUI_brandInfo[0].SetActive(false);
            Canvas_shopUI_details[0].SetActive(false);
            Canvas_shopUI_Archive[0].SetActive(false);
            Canvas_shopUI_Merchandise[0].SetActive(true);
        }
        else if(currentShopName == "re:code_collder")
        {
            Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[1].SetActive(true);
            Canvas_shopUI_brandInfo[1].SetActive(false);
            Canvas_shopUI_details[1].SetActive(false);
            Canvas_shopUI_Archive[1].SetActive(false);
            Canvas_shopUI_Merchandise[1].SetActive(true);
        }
        else if(currentShopName == "cueclyp_collider")
        {
            Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[2].SetActive(true);
            Canvas_shopUI_brandInfo[2].SetActive(false);
            Canvas_shopUI_details[2].SetActive(false);
            Canvas_shopUI_Archive[2].SetActive(false);
            Canvas_shopUI_Merchandise[2].SetActive(true);
        }
        else if(currentShopName == "overlab_collider")
        {
            Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[3].SetActive(true);
            Canvas_shopUI_brandInfo[3].SetActive(false);
            Canvas_shopUI_details[3].SetActive(false);
            Canvas_shopUI_Archive[3].SetActive(false);
            Canvas_shopUI_Merchandise[3].SetActive(true);
        }
    }

    public void pushImageClick()
    {
         if(currentShopName == "re:bud_collider")
        {
            Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[0].SetActive(true);
            Canvas_shopUI_brandInfo[0].SetActive(false);
            Canvas_shopUI_Archive[0].SetActive(false);
            Canvas_shopUI_details[0].SetActive(true);
            Canvas_shopUI_Merchandise[0].SetActive(false);
        }
        else if(currentShopName == "re:code_collder")
        {
            Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[1].SetActive(true);
            Canvas_shopUI_brandInfo[1].SetActive(false);
            Canvas_shopUI_details[1].SetActive(true);
            Canvas_shopUI_Archive[1].SetActive(false);
            Canvas_shopUI_Merchandise[1].SetActive(false);
        }
        else if(currentShopName == "cueclyp_collider")
        {
            Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[2].SetActive(true);
            Canvas_shopUI_brandInfo[2].SetActive(false);
            Canvas_shopUI_details[2].SetActive(true);
            Canvas_shopUI_Archive[2].SetActive(false);
            Canvas_shopUI_Merchandise[2].SetActive(false);
        }
        else if(currentShopName == "overlab_collider")
        {
            Canvas_shopUI.SetActive(true);
            Canvas_shopUI_Logo[3].SetActive(true);
            Canvas_shopUI_brandInfo[3].SetActive(false);
            Canvas_shopUI_details[3].SetActive(true);
            Canvas_shopUI_Archive[3].SetActive(false);
            Canvas_shopUI_Merchandise[3].SetActive(false);
        }
    }

    

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


