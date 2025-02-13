using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonClickHandler : MonoBehaviour
{
    [SerializeField] private GameObject targetPage; // The new page (panel) to open
    [SerializeField] private TMP_Text targetText1;  // First TMP_Text on the target page (Title)
    [SerializeField] private TMP_Text targetText2;  // Second TMP_Text on the target page (Description)
    [SerializeField] private Image targetImage;     // Image on the target page

    private TMP_Text buttonTitle;     // TMP_Text for the title in the button
    private TMP_Text buttonDescription; // TMP_Text for the description in the button
    private Image buttonImage;        // Image on the button

    private void Start()
    {
        // Find the nested TMP_Text and Image components in the button's deep hierarchy
        buttonImage = transform.Find("mask/Image").GetComponent<Image>();
        buttonTitle = transform.Find("Info/title").GetComponent<TMP_Text>();
        buttonDescription = transform.Find("Info/description").GetComponent<TMP_Text>();

        // Add listener to the button click
        GetComponent<Button>().onClick.AddListener(OnButtonClick);

        // Ensure the target page is inactive initially
        targetPage.SetActive(false);
    }

    private void OnButtonClick()
    {
        // Activate the target page
        targetPage.SetActive(true);

        // Transfer the data from the button's children to the new page
        targetText1.text = buttonTitle.text;          // Transfer the title
        targetText2.text = buttonDescription.text;    // Transfer the description
        targetImage.sprite = buttonImage.sprite;      // Transfer the image sprite
    }

    private void OnDestroy()
    {
        // Clean up listener
        GetComponent<Button>().onClick.RemoveListener(OnButtonClick);
    }
}
