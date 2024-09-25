using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActivateToggleOnSelection : MonoBehaviour
{
    [SerializeField] private GameObject targetToggleObject;  // The GameObject containing the target toggle (also has the Image and Toggle components)
    [SerializeField] private GameObject horizontalGroup;     // The parent horizontal layout group
    [SerializeField] private Toggle secondaryToggle;         // Another toggle that needs to be turned off

    private Toggle targetToggle;                             // Reference to the Toggle component in the target toggle GameObject
    private Image targetToggleImage;                         // Reference to the Image component in the target toggle GameObject
    private ToggleImageSelector toggleImageSelector;         // Reference to the ToggleImageSelector component
    private Button[] buttons;                                // Array of buttons automatically fetched from the horizontal group

    void Start()
    {
        // Get the Toggle, Image, and ToggleImageSelector components from targetToggleObject
        targetToggle = targetToggleObject.GetComponent<Toggle>();
        targetToggleImage = targetToggleObject.GetComponent<Image>();
        toggleImageSelector = targetToggleObject.GetComponent<ToggleImageSelector>();

        // Get all Button components in the children of the horizontal group
        buttons = horizontalGroup.GetComponentsInChildren<Button>();

        // Add listener to all buttons in the horizontal group
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;

            // Find the image in each button
            Image buttonImage = buttons[i].transform.Find("Image").GetComponent<Image>();

            // Add listener to handle button click, pass the index to identify the selected image
            buttons[i].onClick.AddListener(() => OnButtonClicked(buttonImage, index));
        }
    }

    // Handle button click
    private void OnButtonClicked(Image buttonImage, int index)
    {
        // If the target toggle GameObject is not active, activate it
        if (!targetToggleObject.activeSelf)
        {
            targetToggleObject.SetActive(true);

            // Start coroutine to update the image after activation
            StartCoroutine(UpdateToggleImageAfterActivation(buttonImage, index));
        }
        else
        {
            // If already active, update the image immediately
            UpdateToggleImage(buttonImage, index);
        }

        // Deactivate the horizontal layout group
        horizontalGroup.SetActive(false);

        // Set the secondary toggle's isOn state to false
        if (secondaryToggle != null && secondaryToggle.isOn)
        {
            secondaryToggle.isOn = false;
        }
    }

    // Coroutine to update the toggle's image after activation
    private IEnumerator UpdateToggleImageAfterActivation(Image buttonImage, int index)
    {
        // Wait for the next frame to ensure the toggle is fully activated
        yield return null;

        // Update the toggle's image and selectedIndex
        UpdateToggleImage(buttonImage, index);
    }

    // Method to update the toggle's image and the selectedIndex in ToggleImageSelector
    private void UpdateToggleImage(Image buttonImage, int index)
    {
        // Update the toggle's image with this button's image sprite
        targetToggleImage.sprite = buttonImage.sprite;

        // Set the ToggleImageSelector's selectedIndex to the index of the selected button
        if (toggleImageSelector != null)
        {
            toggleImageSelector.selectedIndex = index;
        }

        // Optional: Set the toggle's isOn state to true if needed
        if (!targetToggle.isOn)
        {
            targetToggle.isOn = true;
        }
    }
}
