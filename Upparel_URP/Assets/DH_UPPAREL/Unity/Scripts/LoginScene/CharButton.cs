using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharButton : MonoBehaviour ,IPointerClickHandler
{
    public static CharButton currentBtn;
    public Image background;
    public CharacterType type;
    public Action OnEnable;

    public static Action EnableChar;
    public void Enable()
    {
        OnEnable?.Invoke();
        if (currentBtn && background)
        {
            currentBtn.Disable();
        }
        currentBtn = this;
        if(background)
            background.gameObject.SetActive(true);
        EnableChar?.Invoke();
    }
    public void Disable()
    {
        background.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Enable();
    }
}
