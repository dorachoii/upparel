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

    public int characterIndex;
    public int selectedCharacterIndex = -1;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

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
        CharacterSelection.instance.SetSelectedCharacterIndex(characterIndex);
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
