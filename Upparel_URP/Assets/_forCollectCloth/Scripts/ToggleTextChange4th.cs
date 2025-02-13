using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleTextChange4th : MonoBehaviour
{
    private Toggle toggle;

    [SerializeField]
    private Color colorOn = Color.black;  // Default color when toggle is on
    [SerializeField]
    private Color colorOff = new Color32(166, 166, 166, 255); // Default color when toggle is off

    [SerializeField]
    private TMP_Text textTitle;

    private void Awake()
    {
        // Get the Toggle component attached to this GameObject
        toggle = GetComponent<Toggle>();


        if (toggle == null)
        {
            Debug.LogError("Toggle or TMP_Text component is missing.");
            return;
        }

        // Set the initial color based on the current state of the toggle
        UpdateTextColor(toggle.isOn);

        // Add a listener to handle changes in the toggle's state
        toggle.onValueChanged.AddListener(UpdateTextColor);
    }

    private void UpdateTextColor(bool isOn)
    {
        textTitle.color = isOn ? colorOn : colorOff;
    }

}
