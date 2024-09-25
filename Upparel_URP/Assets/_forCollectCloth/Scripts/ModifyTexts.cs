using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModifyTexts : MonoBehaviour
{
    // Reference to the TextMeshProUGUI component on this GameObject
    private TextMeshProUGUI thisText;

    private string lastSelectedTextValue; // To store the last known value of selectedText.text

    [SerializeField]
    private TextMeshProUGUI selectedText; // The TextMeshProUGUI component to monitor

    [SerializeField]
    private GameObject parentLayout; // Reference to the parent object with the Horizontal Layout Group

    // Start is called before the first frame update
    void Start()
    {
        // Automatically find the TextMeshProUGUI component on this GameObject
        thisText = GetComponent<TextMeshProUGUI>();

        if (thisText == null)
        {
            Debug.LogError("TextMeshProUGUI component is missing on this GameObject.");
            return;
        }

        if (selectedText == null)
        {
            Debug.LogError("Selected TextMeshProUGUI is not assigned.");
            return;
        }

        // Initialize lastSelectedTextValue
        lastSelectedTextValue = selectedText.text;

        // Set thisText.text to the initial selectedText.text
        thisText.text = lastSelectedTextValue;

    }

    private void Update()
    {
        // Check if selectedText.text has changed
        if (selectedText.text != lastSelectedTextValue)
        {
            // Update thisText.text
            thisText.text = selectedText.text;

            // Update lastSelectedTextValue
            lastSelectedTextValue = selectedText.text;

        }

        RecalculateLayout();
    }

    // Function to force recalculate layout
    private void RecalculateLayout()
    {
        // Force rebuild layout immediately
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentLayout.GetComponent<RectTransform>());
    }

}
