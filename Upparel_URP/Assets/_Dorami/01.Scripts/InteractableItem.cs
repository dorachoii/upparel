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

        if(other.gameObject.CompareTag("PlayerTag")){

            Transform parent = gameObject.transform.parent;

            if (parent != null) {
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
            ui.Canvas_challenge.SetActive(true);
            AudioClip popup = SoundManager.instance.audioClips[2];
            SoundManager.instance.audioSource_SFX.PlayOneShot(popup);
            PlayerMove.Instance.think();

        }else if(gameObject.CompareTag("Building")){
            AudioClip popup = SoundManager.instance.audioClips[2];
            SoundManager.instance.audioSource_SFX.PlayOneShot(popup);
            ui.ActivateShopUI(gameObject.name);
            PlayerMove.Instance.think();
            
        }else if(gameObject.CompareTag("UpparelCenter")){
            AudioClip popup = SoundManager.instance.audioClips[2];
            SoundManager.instance.audioSource_SFX.PlayOneShot(popup);
            ui.Canvas_center.SetActive(true);
            PlayerMove.Instance.think();
        }
    }

    void OnTriggerExit(Collider other)
    {
        print("태그는" + gameObject.tag);
        selectedOne.SetActive(false);

        if(gameObject.CompareTag("Trash")){
            ui.Canvas_challenge.SetActive(false);
            PlayerMove.Instance.idle();

        }else if(gameObject.CompareTag("Building")){
            ui.DeactivateShopUI(gameObject.name);
            //ui.Canvas_shopUI.SetActive(false);
            PlayerMove.Instance.idle();
            
        }else if(gameObject.CompareTag("UpparelCenter")){
            ui.Canvas_center.SetActive(false);
            PlayerMove.Instance.idle();
        }
    }

}
