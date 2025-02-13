using UnityEngine;
using UnityEngine.UI;

public class ToggleBackgroundSpriteChange : MonoBehaviour
{
    private Toggle toggle; // The Toggle component to manage

    [SerializeField]
    private Sprite spriteOn; // Sprite to display when isOn is true
    [SerializeField]
    private Sprite spriteOff; // Sprite to display when isOn is false

    private Image targetImage; // Assuming the target graphic is an Image

    void Start()
    {
        toggle = GetComponent<Toggle>();
        if (toggle != null)
        {
            targetImage = toggle.targetGraphic as Image; // Get the Image component of the Toggle
            if (targetImage != null)
            {
                // Update sprite initially
                UpdateSprite(toggle.isOn);

                // Register the listener to change sprite when toggle state changes
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
        UpdateSprite(isOn);
    }

    private void UpdateSprite(bool isOn)
    {
        if (targetImage != null)
        {
            // Change the sprite based on whether the toggle is on or off
            targetImage.sprite = isOn ? spriteOn : spriteOff;
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
