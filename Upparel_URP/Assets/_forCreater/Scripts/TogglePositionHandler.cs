using UnityEngine;
using UnityEngine.UI;

public class TogglePositionHandler : MonoBehaviour
{
    [SerializeField] private Toggle toggle;               // The toggle to control
    [SerializeField] private GameObject childObject;      // The child GameObject to display (like the horizontal layout group)
    [SerializeField] private RectTransform toggleRect;    // RectTransform of the toggle
    [SerializeField] private RectTransform childRect;     // RectTransform of the child GameObject
    [SerializeField] private VerticalLayoutGroup layoutGroup;  // The VerticalLayoutGroup of the child object
    [SerializeField] private float offset = 50f;          // Offset to position the child object to the left or right

    private Canvas parentCanvas;

    void Start()
    {
        // Ensure child object is initially hidden
        childObject.SetActive(false);

        // Add listener to toggle to show/hide the child object
        toggle.onValueChanged.AddListener(OnToggleValueChanged);

        // Find the canvas (required to calculate correct screen positions)
        parentCanvas = GetComponentInParent<Canvas>();
    }

    // This is called when the toggle value changes
    private void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            // Show the child object
            childObject.SetActive(true);

            // Adjust the child object's position based on the toggle's screen position
            AdjustChildPositionAndAlignment();
        }
        else
        {
            // Hide the child object
            childObject.SetActive(false);
        }
    }

    // Adjusts the child object's position and layout group alignment based on the toggle's position
    private void AdjustChildPositionAndAlignment()
    {
        // Get the screen position of the toggle
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(parentCanvas.worldCamera, toggleRect.position);

        // Check if the toggle is on the left or right half of the screen
        float screenWidth = Screen.width;

        if (screenPos.x > screenWidth / 2)
        {
            // Toggle is on the right half of the screen
            // Place child object on the left and set alignment to Upper Right
            childRect.anchoredPosition = new Vector2(-offset, 0);
            layoutGroup.childAlignment = TextAnchor.UpperRight;  // Set alignment to Upper Right
        }
        else
        {
            // Toggle is on the left half of the screen
            // Place child object on the right and set alignment to Upper Left
            childRect.anchoredPosition = new Vector2(offset, 0);
            layoutGroup.childAlignment = TextAnchor.UpperLeft;   // Set alignment to Upper Left
        }
    }

    private void OnDestroy()
    {
        // Clean up the listener when the object is destroyed
        toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }
}
 