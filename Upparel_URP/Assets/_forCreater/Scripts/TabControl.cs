using UnityEngine;
using UnityEngine.UI;

public class TabBarControl : MonoBehaviour
{
    [System.Serializable]
    public class TabItem
    {
        public Toggle toggle;      // The toggle control
        public GameObject tabView; // The corresponding tab view
    }

    [SerializeField] private TabItem[] tabs; // Array of tabs

    private void Start()
    {
        // Add listener to each toggle
        foreach (var tab in tabs)
        {
            if (tab.toggle != null && tab.tabView != null)
            {
                tab.toggle.onValueChanged.AddListener((isOn) => OnToggleValueChanged(tab, isOn));
            }
            else
            {
                Debug.LogError("Toggle or tabView is not assigned in TabItem.");
            }
        }

        // Initialize the tab views based on the toggles' initial states
        UpdateTabViews();
    }

    private void OnToggleValueChanged(TabItem tab, bool isOn)
    {
        // Activate or deactivate the corresponding tab view based on toggle's isOn
        tab.tabView.SetActive(isOn);
    }

    private void UpdateTabViews()
    {
        // Update all tab views based on the toggles' current states
        foreach (var tab in tabs)
        {
            if (tab.toggle != null && tab.tabView != null)
            {
                tab.tabView.SetActive(tab.toggle.isOn);
            }
        }
    }

    private void OnDestroy()
    {
        // Remove listeners to prevent memory leaks
        foreach (var tab in tabs)
        {
            if (tab.toggle != null)
            {
                tab.toggle.onValueChanged.RemoveAllListeners();
            }
        }
    }
}
