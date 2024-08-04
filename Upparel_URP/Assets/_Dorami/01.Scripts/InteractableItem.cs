using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    GameObject selectedOne;
    UIManager ui;

    private void Start() {
        ui = UIManager.instance;
    }

    private void OnTriggerEnter(Collider other) {

        print("부딪힌 오브젝트의 이름은" + other.gameObject.tag);

        if(other.gameObject.CompareTag("PlayerTag")){

            Transform parent = gameObject.transform.parent;
            print("parent의 이름은" + parent);

            if (parent != null) {
                print("parent는 null이 아니다!");
                foreach (Transform child in parent) {
                    if (child.CompareTag("Selected")) {

                        selectedOne = child.gameObject;
                        selectedOne.SetActive(true);
                        break;
                    }
                }
            }
        }

        print("태그는" + gameObject.tag);
        

        if(gameObject.CompareTag("Trash")){
            ui.Btn_Funding.SetActive(true);
        }else if(gameObject.CompareTag("Building")){
            
            ui.Btn_Donation.SetActive(true);
            ui.Btn_Tracking.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        print("태그는" + gameObject.tag);
        selectedOne.SetActive(false);

        if(gameObject.CompareTag("Trash")){
            ui.Btn_Funding.SetActive(false);

        }else if(gameObject.CompareTag("Building")){
            ui.Btn_Donation.SetActive(false);
            ui.Btn_Tracking.SetActive(false);
        }
    }

}
