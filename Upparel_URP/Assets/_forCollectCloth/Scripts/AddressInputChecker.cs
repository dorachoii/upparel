using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddressInputChecker : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField1;
    [SerializeField] private TMP_InputField inputField2;
    [SerializeField] private Button buttonCheck;
    [SerializeField] private Button buttonDone;

    private void Start()
    {
        // Add listeners to the TMP_InputFields to check their content when changed
        inputField1.onValueChanged.AddListener(delegate { CheckInputFields(); });
        inputField2.onValueChanged.AddListener(delegate { CheckInputFields(); });

        // Initial check to set buttons state based on current input fields' values
        CheckInputFields();
    }

    private void CheckInputFields()
    {
        // Check if the first input field is filled
        bool isInputField1Filled = !string.IsNullOrEmpty(inputField1.text);
        // Check if both input fields are filled
        bool areBothInputFieldsFilled = isInputField1Filled && !string.IsNullOrEmpty(inputField2.text);

        // Set button A interactable if the first input field is filled
        buttonCheck.interactable = isInputField1Filled;

        // Set button B interactable if both input fields are filled
        buttonDone.interactable = areBothInputFieldsFilled;
    }
}
