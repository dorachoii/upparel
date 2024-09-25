using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Modify4thTexts : MonoBehaviour
{
    private TextMeshProUGUI childText;
    private Toggle thisToggle;

    [SerializeField]
    private TextHolder textHolder; // Reference to the TextHolder script

    [SerializeField]
    private GameObject additionalInfo; // Text component to display "AM"/"PM"

    public enum DataType
    {
        Day,
        Time,
        Location
    }

    [SerializeField]
    private DataType dataType; // Specify which data type this toggle handles

    [SerializeField]
    private bool isMorningToggle = true; // Set to true if this toggle represents morning time

    void Start()
    {
        thisToggle = GetComponent<Toggle>();
        childText = GetComponentInChildren<TextMeshProUGUI>();

        if (thisToggle == null || childText == null || textHolder == null)
        {
            Debug.LogError("Missing components in Modify4thTexts script.");
            return;
        }

        thisToggle.onValueChanged.AddListener(OnToggleValueChanged);

        // Check initial state
        if (thisToggle.isOn)
        {
            UpdateTextHolder();
        }
    }

    void OnDestroy()
    {
        if (thisToggle != null)
        {
            thisToggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }
    }

    private void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            UpdateTextHolder();
        }
    }

    private void UpdateTextHolder()
    {
        if (textHolder != null && childText != null)
        {
            switch (dataType)
            {
                case DataType.Day:
                    additionalInfo.SetActive(true);
                    textHolder.dayString = childText.text;
                    break;
                case DataType.Time:
                    additionalInfo.SetActive(true);
                    textHolder.timeString = childText.text;
                    textHolder.isMorning = isMorningToggle; // Set isMorning based on the toggle
                    break;
                case DataType.Location:
                    // Location is handled separately
                    break;
                default:
                    Debug.LogWarning("Unknown data type in Modify4thTexts");
                    break;
            }
        }
    }
}
