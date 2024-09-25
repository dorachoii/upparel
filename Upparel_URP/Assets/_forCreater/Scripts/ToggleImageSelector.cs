using UnityEngine;
using UnityEngine.UI;

public class ToggleImageSelector : MonoBehaviour
{
    [SerializeField] private Toggle toggle;                       // The toggle to open the image group
    [SerializeField] private GameObject imageGroup;               // The horizontal layout group with images
    [SerializeField] private Image toggleImage;                   // The image in the toggle to update
    [SerializeField] private Sprite defaultToggleSprite;          // Default sprite of the toggle before selection
    [SerializeField] private GameObject[] masks;                  // Array of mask GameObjects (parent of image and selectionIndicator)

    public int selectedIndex = 0;                                // Index of the selected image, default is the first

    void Start()
    {
        // Initially hide the image group
        imageGroup.SetActive(false);

        // Add listener to toggle to show/hide the image group
        toggle.onValueChanged.AddListener(OnToggleValueChanged);

        // Initialize selection indicators and select the first image by default
        for (int i = 0; i < masks.Length; i++)
        {
            int index = i;

            // Find the image and selection indicator in the mask object
            Image image = masks[i].transform.Find("Image").GetComponent<Image>();
            GameObject selectionIndicator = masks[i].transform.Find("selectionIndicator").gameObject;

            // Deactivate the selection indicator initially
            selectionIndicator.SetActive(i == selectedIndex); // Highlight the first image initially

            // Add listener to each mask's button to handle selection
            masks[i].GetComponent<Button>().onClick.AddListener(() => OnImageSelected(index, image, selectionIndicator));
        }

        // Set the toggle's image to the first image in the array by default
        toggleImage.sprite = masks[selectedIndex].transform.Find("Image").GetComponent<Image>().sprite;
    }

    // Handle the toggle value change
    private void OnToggleValueChanged(bool isOn)
    {
        imageGroup.SetActive(isOn); // Show or hide the image group when the toggle is selected

        if (isOn)
        {
            // Highlight the currently selected image (first image by default)
            UpdateSelectionIndicators(selectedIndex);
        }
    }

    // Handle image selection
    private void OnImageSelected(int index, Image selectedImage, GameObject selectionIndicator)
    {
        selectedIndex = index;  // Store the selected index

        // Update the toggle's image with the selected image sprite
        toggleImage.sprite = selectedImage.sprite;

        // Set the toggle's isOn state to the opposite
        toggle.isOn = false;

        // Close the image group after selection
        imageGroup.SetActive(false);

        // Update selection indicators to highlight the selected image
        UpdateSelectionIndicators(index);
    }

    // Update the visibility of selection indicators
    private void UpdateSelectionIndicators(int selectedIndex)
    {
        for (int i = 0; i < masks.Length; i++)
        {
            // Find the selection indicator in the mask object
            GameObject selectionIndicator = masks[i].transform.Find("selectionIndicator").gameObject;

            // Activate the selection indicator for the selected image, deactivate others
            selectionIndicator.SetActive(i == selectedIndex);
        }
    }
}
