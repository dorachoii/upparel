using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateTag : MonoBehaviour
{
    // Reference to the TextMeshProUGUI component
    private TextMeshProUGUI textMeshProUI;

    // Reference to the toggle
    private Toggle toggle;

    [SerializeField]
    private GameObject selectedTag;

    [SerializeField]
    private TextMeshProUGUI selectedText;

    [SerializeField]
    private GameObject parentLayout; // Reference to the parent object with the Horizontal Layout Group




    // Start is called before the first frame update
    void Start()
    {
        // Automatically find the TextMeshProUGUI component in the parent
        textMeshProUI = GetComponentInChildren<TextMeshProUGUI>();

        // Get the Toggle component attached to the same GameObject
        toggle = GetComponent<Toggle>();

        if (toggle != null)
        {
            // Register the TagCreate method to the toggle's OnValueChanged event
            toggle.onValueChanged.AddListener(TagCreate);
        }
        else
        {
            Debug.LogError("No Toggle component found in parent GameObject.");
        }
    }

    private void Update()
    {
        RecalculateLayout();
    }

    // Method to update the selected tag's state and text based on the toggle's state
    public void TagCreate(bool isOn)
    {
        if (selectedTag != null)
        {
            // Set the active state of selectedTag based on the toggle's isOn value
            selectedTag.SetActive(isOn);
        }

        if (isOn && selectedText != null && textMeshProUI != null)
        {
            // Update the text only when the toggle is turned on
            selectedText.text = textMeshProUI.text;
        }
    }

    void OnDestroy()
    {
        // Unregister the listener to avoid memory leaks
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(TagCreate);
        }
    }


    // Function to force recalculate layout
    private void RecalculateLayout()
    {
        // Force rebuild layout immediately
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentLayout.GetComponent<RectTransform>());
    }
}
