using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ButtonController : MonoBehaviour
{
    private int contentNumber; // Holds the current active content number
    private int stageNumber;   // Holds the current active stage number

    [SerializeField]
    private GameObject[] contents;       // Array to hold content GameObjects

    [SerializeField]
    private GameObject[] descriptions;   // Array to hold description GameObjects

    [SerializeField]
    private GameObject[] stagesName;     // Array to hold stage name GameObjects

    [SerializeField]
    private Image[] stagesBox;           // Array to hold stage box Images

    [SerializeField]
    private Image[] lines;               // Array to hold line Images

    [SerializeField]
    private GameObject parentLayout;     // Reference to the parent object with the Horizontal Layout Group

    [SerializeField]
    private GameObject enter_info_page;  // Reference to the enter info page GameObject

    [SerializeField]
    private GameObject final_page;

    [SerializeField]
    private ContentChecker contentChecker; // Reference to the ContentChecker script

    [SerializeField]
    private Color keyColor;              // Color to represent key stages

    [SerializeField]
    private Color disableColor;          // Color to represent disabled stages

    [SerializeField]
    private CameraControllerForOCR cameraControllerForOCR;   // For the second use case (image sent for OCR)

    [SerializeField]
    private GameObject[] topInfos;     // Reference to the parent object with the Horizontal Layout Group

    private TextMeshProUGUI buttonText;

    public string[] buttonTextInfo = new string[] { "다음", "스캔하기", "의류 수거 신청하기", "선택하기"};



    // Start is called before the first frame update
    void Start()
    {
        contentNumber = 0; // Initialize with the first content
        UpdateContent();   // Set initial content visibility
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        RecalculateLayout();
    }

    public void NextContent()
    {
        if (contentNumber == 1)
        {
            buttonText.text = buttonTextInfo[1];
        }
        else if (contentNumber % 2 == 0 && contentNumber > 2)
        {

            buttonText.text = buttonTextInfo[3];
        }
        else if (contentNumber % 2 == 1 && contentNumber > 3)
        {

            buttonText.text = buttonTextInfo[2];
        }
        else
        {
            buttonText.text = buttonTextInfo[0];
        }


        if (stageNumber <= 2)
        {
            ChangeTopInfoUp(stageNumber);
        }

        if (contentNumber == 2) // At the point you want to use the OCR camera controller
        {
            // Open the camera session
            cameraControllerForOCR.OpenCameraSession();

            // Start a coroutine to wait until the camera session is complete
            StartCoroutine(WaitForCameraSession(cameraControllerForOCR));
        }
        else
        {
            ProceedToNextContent();
        }
    }

    private IEnumerator WaitForCameraSession(MonoBehaviour cameraController)
    {
        // Wait until the camera session is complete
        while (true)
        {
            if (cameraController is CameraController cc && cc.IsSessionComplete)
            {
                break;
            }
            else if (cameraController is CameraControllerForOCR ccOCR && ccOCR.IsSessionComplete)
            {
                break;
            }

            yield return null;
        }

        // Proceed to the next content after the camera session is complete
        ProceedToNextContent();
    }

    public void ChangeTopInfoUp(int stageNumber)
    {
        if(stageNumber == 0)
        {
            topInfos[stageNumber].SetActive(false);
        }

        topInfos[stageNumber + 1].SetActive(true);

    }
    public void ChangeTopInfoDown(int stageNumber)
    {
        topInfos[stageNumber].SetActive(false);

    }

    private void ProceedToNextContent()
    {
        // Deactivate current content
        contents[contentNumber].SetActive(false);
        descriptions[contentNumber].SetActive(false);

        contentChecker.contentStarted = false;

        if (contentChecker.contentFinished == true)
        {
            stagesName[stageNumber].SetActive(false);
            stageNumber++;
        }

        // Move to the next content
        contentNumber++;

        // Check if we have reached the end of the contents array
        if (contentNumber >= contents.Length)
        {
            // We've reached the end, hide the enter info page and navigation button
            enter_info_page.SetActive(false);
            final_page.SetActive(true);
        }
        else
        {
            // Activate the new content
            contents[contentNumber].SetActive(true);
            descriptions[contentNumber].SetActive(true);
        }

        if (contentChecker.contentFinished == true)
        {
            stagesBox[stageNumber].color = keyColor;
            lines[stageNumber].color = keyColor;
            stagesName[stageNumber].SetActive(true);

            contentChecker.contentFinished = false;
            contentChecker.contentStarted = true;
        }
    }

    public void PrevContent()
    {
        if (contentNumber == 0)
        {
            enter_info_page.SetActive(false);
        }

        else
        {

            if (stageNumber <= 3 && stageNumber >= 1)
            {
                ChangeTopInfoDown(stageNumber);
            }

            // Deactivate current content
            contents[contentNumber].SetActive(false);
            descriptions[contentNumber].SetActive(false);

            if (contentChecker.contentStarted == true)
            {
                stagesName[stageNumber].SetActive(false);
                stagesBox[stageNumber].color = disableColor;
                lines[stageNumber].color = disableColor;

                stageNumber--;
            }

            // Move to the previous content
            contentNumber--;

            // Activate the new content
            contents[contentNumber].SetActive(true);
            descriptions[contentNumber].SetActive(true);

            if (contents[contentNumber].CompareTag("FirstUI"))
            {
                contentChecker.contentStarted = true;
            }

            if (contentChecker.contentStarted == true)
            {
                stagesName[stageNumber].SetActive(true);
            }
        }

        // Show the navigation button again when going back from the last content
        //navigationButton.gameObject.SetActive(true);
    }

    // Method to update content visibility
    private void UpdateContent()
    {
        for (int i = 0; i < contents.Length; i++)
        {
            contents[i].SetActive(i == contentNumber); // Activate the current content, deactivate others
        }
        for (int i = 0; i < stagesName.Length; i++)
        {
            stagesName[i].SetActive(i == contentNumber);
        }
    }

    // Function to force recalculate layout
    private void RecalculateLayout()
    {
        // Force rebuild layout immediately
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentLayout.GetComponent<RectTransform>());
    }
}
