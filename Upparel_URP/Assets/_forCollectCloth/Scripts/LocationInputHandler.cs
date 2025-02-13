using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocationInputHandler : MonoBehaviour
{
    [SerializeField]
    private TextHolder textHolder; // Reference to the TextHolder script

    private TMP_InputField inputField;

    void Awake()
    {
        inputField = GetComponent<TMP_InputField>();

        if (inputField == null || textHolder == null)
        {
            Debug.LogError("Missing components in LocationInputHandler script.");
            return;
        }

        // Subscribe to the input field's onValueChanged event
        inputField.onValueChanged.AddListener(OnInputValueChanged);

        // Alternatively, subscribe to onEndEdit if you prefer to update when input ends
        // inputField.onEndEdit.AddListener(OnInputEndEdit);
    }

    void OnDestroy()
    {
        if (inputField != null)
        {
            inputField.onValueChanged.RemoveListener(OnInputValueChanged);
            // inputField.onEndEdit.RemoveListener(OnInputEndEdit);
        }
    }

    private void OnInputValueChanged(string input)
    {
        textHolder.locationString = input;
    }

    // If using onEndEdit
    /*
    private void OnInputEndEdit(string input)
    {
        textHolder.locationString = input;
    }
    */
}
