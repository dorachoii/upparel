using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro; // Include the TextMeshPro namespace
using UnityEngine.UI;

public class ToggleGroupController : MonoBehaviour
{
    private ToggleGroup toggleGroup; // The ToggleGroup component

    [SerializeField]
    private Button targetButton; // The button whose interactability is controlled

    [SerializeField]
    private ContentChecker contentChecker;

    void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();

        if (toggleGroup == null || targetButton == null)
        {
            Debug.LogError("ToggleGroup or Button not assigned.");
            return;
        }

        // Set all toggles in the group to be off initially
        foreach (Toggle toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            toggle.isOn = false;
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        // Initial check
        UpdateButtonInteractability();
    }

    private void OnToggleValueChanged(bool isOn)
    {
        // Log the GameObject name of the toggle if it is on
        if (isOn)
        {
            Debug.Log("Toggle On: " + UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
        }

        UpdateButtonInteractability();
    }

    private void UpdateButtonInteractability()
    {
        // Check if any toggle in the group is on
        bool anyButtonOn = false;
        foreach (Toggle toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            if (toggle.isOn)
            {
                anyButtonOn = true;
                contentChecker.contentFinished = anyButtonOn;
                break;
            }
        }

        // Set the interactability of the button based on toggle states
        targetButton.interactable = anyButtonOn;
    }

    void OnDestroy()
    {
        // Clean up listeners to avoid potential memory leaks
        foreach (Toggle toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }
    }
}
