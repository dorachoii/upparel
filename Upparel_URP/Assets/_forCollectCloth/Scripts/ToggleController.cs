using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Include the TextMeshPro namespace
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public string targetTag; // Tag to search for
    private GameObject targetObject; // The GameObject to activate/deactivate

    private Toggle toggle; // The toggle component attached to this GameObject

    void Start()
    {
        // Get the Toggle component attached to this GameObject
        toggle = GetComponent<Toggle>();

        // Automatically find the target object with the specified tag
        if (!string.IsNullOrEmpty(targetTag))
        {
            targetObject = GameObject.FindWithTag(targetTag);

            // Check if the Toggle and targetObject are found
            if (toggle != null && targetObject != null)
            {
                // Register the OnToggleValueChanged method to the toggle's onValueChanged event
                toggle.onValueChanged.AddListener(OnToggleValueChanged);
            }
            else
            {
                Debug.LogError("Toggle component or target object with tag '" + targetTag + "' not found on this GameObject.");
            }
        }

        // Method called when the toggle value changes
        void OnToggleValueChanged(bool isOn)
        {
            if (targetObject != null)
            {
                // Set the active state of the target object based on the toggle's isOn value
                targetObject.SetActive(isOn);
            }
        }

        void OnDestroy()
        {
            // Unregister the listener to avoid memory leaks
            toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }
    }
}
