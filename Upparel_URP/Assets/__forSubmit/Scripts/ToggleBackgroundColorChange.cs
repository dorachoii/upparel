using UnityEngine;
using UnityEngine.UI;

public class ToggleBackgroundColorChange : MonoBehaviour
{
    private Toggle toggle; // The Toggle component to manage

    [SerializeField]
    private Color colorOn = Color.white; // Color when isOn is true
    [SerializeField]
    private Color colorOff = new Color(0.9569f, 0.9569f, 0.9647f); // Color when isOn is false (Hex #F4F4F6)

    private Image targetImage; // Assuming the target graphic is an Image

    void Start()
    {
        toggle = GetComponent<Toggle>();
        if (toggle != null)
        {
            targetImage = toggle.targetGraphic as Image; // Get the Image component of the Toggle
            if (targetImage != null)
            {
                // Update color initially
                UpdateColor(toggle.isOn);

                // Register the listener to change color when toggle state changes
                toggle.onValueChanged.AddListener(OnToggleValueChanged);
            }
            else
            {
                Debug.LogError("No Image component found on the Toggle's target graphic.");
            }
        }
        else
        {
            Debug.LogError("Toggle component is not assigned.");
        }
    }

    private void OnToggleValueChanged(bool isOn)
    {
        UpdateColor(isOn);
    }

    private void UpdateColor(bool isOn)
    {
        if (targetImage != null)
        {
            targetImage.color = isOn ? colorOn : colorOff;
        }
    }

    void OnDestroy()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }
    }
}
