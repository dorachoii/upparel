using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //JoyStick UI 할당
    public RectTransform centerArea = null;
    public RectTransform handle = null;
     // InputControl ❓
    [InputControl(layout = "Vector2")]

    private string stickControlPath;
    private float movementRange = 100f;

    protected VirtualJoystickType joystickType = VirtualJoystickType.Fixed;
    protected bool _hideOnPointerUp = false;
    protected bool _centralizeOnPointerUp = true;
    
    private Canvas canvas;

    // JoyStick의 위치
    protected RectTransform baseRect = null;
    // OnScreenStick ❓ - movementRange랑 stickControlPath 요소 무슨 뜻인지
    protected OnScreenStick handleStickController = null;
    // CanvasGroup❓
    protected CanvasGroup bgCanvasGroup = null;
    protected Vector2 initialPos = Vector2.zero;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        baseRect = GetComponent<RectTransform>();
        bgCanvasGroup = centerArea.GetComponent<CanvasGroup>();
        handleStickController = handle.gameObject.AddComponent<OnScreenStick>();
        handleStickController.movementRange = movementRange;
        handleStickController.controlPath = stickControlPath;

        Vector2 center = new Vector2(0.5f, 0.5f);
        centerArea.pivot = center;
        // Anchor min,max ❓
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;

        initialPos = centerArea.anchoredPosition;

        centerArea.anchoredPosition = initialPos;
        bgCanvasGroup.alpha = 1;
    }

    public void OnPointerDown(PointerEventData eventData){
        PointerEventData constructedEventData = new PointerEventData(EventSystem.current);
        constructedEventData.position = handle.position;
        handleStickController.OnPointerDown(constructedEventData);
    }

    public void OnPointerUp(PointerEventData eventData){
        PointerEventData constructedEventData = new PointerEventData(EventSystem.current);
        constructedEventData.position = Vector2.zero;

        handleStickController.OnPointerUp(constructedEventData);
    }

    protected Vector2 GetAnchoredPos(Vector2 screenPosition){
        Camera cam = (canvas.renderMode == RenderMode.ScreenSpaceCamera) ? canvas.worldCamera : null;
        Vector2 localPoint = Vector2.zero;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint)){
            Vector2 pivotOffset = baseRect.pivot * baseRect.sizeDelta;
            return localPoint - (centerArea.anchorMax * baseRect.sizeDelta) + pivotOffset;
        }

        return Vector2.zero;
    }

}
