using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleTextColorChanger : MonoBehaviour
{
    private Toggle toggle;
    private TMP_Text[] texts; // Array of TextMeshPro text components

    [SerializeField] private Color colorOn = Color.white;  // Default color when toggle is on
    [SerializeField] private Color colorOff = new Color32(166, 166, 166, 255); // Default color when toggle is off

    private void Awake()
    {
        // Get the Toggle component attached to this GameObject
        toggle = GetComponent<Toggle>();

        // Find all TMP_Text components in the children of this GameObject
        texts = GetComponentsInChildren<TMP_Text>();

        // Ensure both components are found
        if (toggle == null || texts == null || texts.Length == 0)
        {
            Debug.LogError("Toggle or TMP_Text components are missing.");
            return;
        }

        // Set the initial color based on the current state of the toggle
        UpdateTextColor(toggle.isOn);

        // Add a listener to handle changes in the toggle's state
        toggle.onValueChanged.AddListener(UpdateTextColor);
    }

    private void UpdateTextColor(bool isOn)
    {
        // Change the color of each text based on whether the toggle is on or off
        foreach (var text in texts)
        {
            text.color = isOn ? colorOn : colorOff;
        }
    }

    private void OnDestroy()
    {
        // Remove the listener when the script is destroyed to avoid memory leaks
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(UpdateTextColor);
        }
    }
}
