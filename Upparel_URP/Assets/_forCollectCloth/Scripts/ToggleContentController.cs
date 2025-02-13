using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ToggleContentController : MonoBehaviour
{
    [System.Serializable]
    public class ToggleContent
    {
        public Toggle toggle;
        public GameObject content;
    }

    public List<ToggleContent> toggleContents; // Assign in the inspector

    private void Start()
    {
        // Initialize each toggle
        foreach (var tc in toggleContents)
        {
            tc.toggle.onValueChanged.AddListener((state) => ToggleChanged(tc.toggle, tc.content, state));
            // Initialize the content visibility based on the toggle's initial state
            tc.content.SetActive(tc.toggle.isOn);
        }
    }

    private void ToggleChanged(Toggle toggle, GameObject content, bool state)
    {
        // Set the content active state to match the toggle state
        content.SetActive(state);
    }
}
