using UnityEngine;
using UnityEngine.UI;

public class ToggleSelectedLine : MonoBehaviour
{
    private Toggle toggle;           // Reference to the Toggle component
    private GameObject selectedLine;                  // Reference to the "selected_line" child

    void Start()
    {
        toggle = GetComponent<Toggle>();

        // Find the "selected_line" child GameObject
        selectedLine = transform.Find("selected_line").gameObject;

        if (selectedLine == null)
        {
            Debug.LogError("No child named 'selected_line' found in this GameObject.");
            return;
        }

        // Register the listener for the Toggle's onValueChanged event
        toggle.onValueChanged.AddListener(OnToggleValueChanged);

        // Set the initial state of "selected_line" based on the Toggle's isOn state
        selectedLine.SetActive(toggle.isOn);
    }

    // This method is called when the Toggle's value changes
    private void OnToggleValueChanged(bool isOn)
    {
        // Activate or deactivate "selected_line" based on the Toggle's isOn state
        selectedLine.SetActive(isOn);
    }

    void OnDestroy()
    {
        // Clean up the listener when the object is destroyed
        toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }
}
