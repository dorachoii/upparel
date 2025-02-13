using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Include the TextMeshPro namespace
using UnityEngine.UI;

public class ToggleImageColorChanger : MonoBehaviour
{
    private Toggle toggle; // The Toggle component to manage

    [SerializeField]
    private Color colorOn = Color.black; // Color when isOn is true
    [SerializeField]
    private Color colorOff = new Color(0.651f, 0.651f, 0.651f); // Color when isOn is false (Hex #A6A6A6)

    //private TextMeshProUGUI targetText; // Reference to the TextMeshProUGUI component
    private Image image;

    void Start()
    {
        toggle = GetComponent<Toggle>();

        if (toggle != null)
        {
            // Find the TextMeshProUGUI component in the children of this GameObject
            //targetText = GetComponentInChildren<TextMeshProUGUI>();
            image = GetComponentInChildren<Image>();

            if (image != null)
            {
                // Update color initially
                UpdateColor(toggle.isOn);

                // Register the listener to change color when toggle state changes
                toggle.onValueChanged.AddListener(OnToggleValueChanged);
            }
            else
            {
                Debug.LogError("No TextMeshProUGUI component found in the children of this GameObject.");
            }
        }
        else
        {
            Debug.LogError("Toggle component is not assigned.");
        }
    }

    private void OnToggleValueChanged(bool isOn)
    {
        UpdateColor(isOn);
    }

    private void UpdateColor(bool isOn)
    {
        //if (targetText != null)
        //{
        //    targetText.color = isOn ? colorOn : colorOff;
        //}
        if (image != null)
        {
            image.color = isOn ? colorOn : colorOff;
        }
    }

    void OnDestroy()
    {
        if (toggle != null)
        {
            toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }
    }
}
