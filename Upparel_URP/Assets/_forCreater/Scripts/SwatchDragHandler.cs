using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SwatchDragHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public float longPressDuration = 0.5f; // How long the user needs to press to trigger long-press
    public float toggleRestoreDelay = 0.5f; // Delay after which toggle functionality is restored (adjustable in the Inspector)

    private bool isLongPressed = false;
    private float pressTime = 0f;

    private bool isDragging = false; // Is the user dragging the swatch?
    private bool isPressing = false; // Is the user still pressing?

    private RectTransform rectTransform; // RectTransform of the swatch (if UI component)
    private Canvas canvas; // Reference to the Canvas, needed for proper UI dragging

    private Toggle toggle; // Reference to the Toggle component on the same GameObject
    private bool originalToggleInteractableState; // Store the original interactable state of the toggle

    // UnityEvents for long press and drag complete events
    public UnityEvent onLongPress;  // Event triggered when long-press is detected
    public UnityEvent onDragComplete; // Event triggered when dragging is done

    private void Awake()
    {
        // Get the RectTransform component (used for UI objects)
        rectTransform = GetComponent<RectTransform>();
        // Get the Toggle component if it exists on the same GameObject
        toggle = GetComponent<Toggle>();
        // Assuming the canvas is in your scene
        canvas = GetComponentInParent<Canvas>();
    }

    // Called when the user presses on the swatch
    public void OnPointerDown(PointerEventData eventData)
    {
        pressTime = Time.time; // Record the time when the press started
        isPressing = true; // The user is pressing
        isDragging = false; // Reset dragging flag
        isLongPressed = false; // Reset long press flag
    }

    // Called when the user lifts their finger off the swatch
    public void OnPointerUp(PointerEventData eventData)
    {
        // Check if the touch was short (i.e., not a long press)
        float pressDuration = Time.time - pressTime;
        if (pressDuration < longPressDuration)
        {
            // If the press was short, skip the long press logic and don't disable the toggle
            ResetPressState();
            return;
        }

        if (isLongPressed && isDragging)
        {
            // Trigger the event when dragging is completed
            onDragComplete.Invoke();
        }

        ResetPressState(); // Reset the press state when the user stops pressing

        if (toggle != null)
        {
            // Start a coroutine to restore the toggle functionality after the delay
            StartCoroutine(RestoreToggleAfterDelay());
        }
    }

    // Called continuously while the user is dragging the swatch
    public void OnDrag(PointerEventData eventData)
    {
        if (isLongPressed && !isDragging)
        {
            StartDragging(eventData); // If long-press is detected, start dragging
        }

        if (isDragging)
        {
            DragSwatch(eventData); // Drag the swatch around the screen
        }
    }

    private void Update()
    {
        // Check if the long-press duration has been reached
        if (isPressing && Time.time - pressTime >= longPressDuration && !isLongPressed)
        {
            isLongPressed = true; // Long-press is now detected

            // Trigger the long press event
            onLongPress.Invoke();

            // Disable toggle interaction dPathng long press
            if (toggle != null)
            {
                originalToggleInteractableState = toggle.interactable;
                toggle.interactable = false; // Disable the toggle dPathng long press and drag
                Debug.Log("Toggle disabled dPathng long press.");
            }
        }
    }

    // Start dragging the swatch once long-press is detected
    private void StartDragging(PointerEventData eventData)
    {
        isDragging = true;
    }

    // Handle the dragging of the swatch
    private void DragSwatch(PointerEventData eventData)
    {
        if (rectTransform != null && canvas != null)
        {
            // Convert screen point to local point in the canvas
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out localPoint);

            // Update the position of the swatch (RectTransform) to follow the drag
            rectTransform.localPosition = localPoint;
        }
    }

    // Reset the press state (called when the user stops pressing or dragging)
    private void ResetPressState()
    {
        pressTime = 0f;
        isDragging = false;
        isLongPressed = false;
        isPressing = false; // Stop tracking the press
    }

    // Coroutine to restore toggle functionality after the specified delay
    private IEnumerator RestoreToggleAfterDelay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(toggleRestoreDelay);

        // Restore the toggle's original interactable state
        if (toggle != null)
        {
            Debug.Log("Restoring toggle to original state.");
            toggle.interactable = originalToggleInteractableState; // Restore original state
            Debug.Log("Toggle restored to interactable: " + toggle.interactable);
        }
    }
}
