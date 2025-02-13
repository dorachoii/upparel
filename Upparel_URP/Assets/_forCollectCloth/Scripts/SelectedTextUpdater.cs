using UnityEngine;
using TMPro;

public class SelectedTextUpdater : MonoBehaviour
{
    [SerializeField]
    private TextHolder textHolder; // Reference to the TextHolder script

    [SerializeField]
    private TextMeshProUGUI additionalInfoText; // Text component to display "AM"/"PM"

    private TextMeshProUGUI textComponent;

    public enum DataType
    {
        Day,
        Time,
        Location
    }

    [SerializeField]
    private DataType dataType; // Specify which data type this text handles

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();

        if (textComponent == null || textHolder == null)
        {
            Debug.LogError("Missing components in SelectedTextUpdater script.");
            return;
        }

        if (additionalInfoText == null)
        {
            Debug.LogError("AdditionalInfoText is not assigned.");
        }
    }

    void OnEnable()
    {
        UpdateText();
    }

    private void Update()
    {
        UpdateAdditionalInfo();
    }

    private void UpdateText()
    {
        if (textHolder != null && textComponent != null)
        {
            switch (dataType)
            {
                case DataType.Day:
                    textComponent.text = textHolder.dayString;
                    break;
                case DataType.Time:
                    textComponent.text = textHolder.timeString;
                    break;
                case DataType.Location:
                    textComponent.text = textHolder.locationString;
                    break;
                default:
                    Debug.LogWarning("Unknown data type in SelectedTextUpdater");
                    break;
            }
        }
    }

    private void UpdateAdditionalInfo()
    {
        if (additionalInfoText != null && textComponent.text != null)
        {
            switch (dataType)
            {
                case DataType.Day:
                    additionalInfoText.text = "2024.7.";
                    break;
                case DataType.Time:
                    additionalInfoText.text = textHolder.isMorning ? "오전" : "오후";
                    break;
                case DataType.Location:
                    break;
                default:
                    Debug.LogWarning("Unknown data type in SelectedTextUpdater");
                    break;
            }
        }
    }
}
