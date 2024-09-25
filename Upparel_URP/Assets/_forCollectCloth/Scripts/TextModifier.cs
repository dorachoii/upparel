using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


public class TextModifier : MonoBehaviour
{
    [SerializeField]
    private GameObject parentLayout;     // Reference to the parent object with the Horizontal Layout Group

    [SerializeField]
    private TextMeshProUGUI selectedText;

    private TextMeshProUGUI thisText;

    // Start is called before the first frame update
    void Start()
    {
        thisText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        thisText.text = selectedText.text;
        RecalculateLayout();
    }

    private void RecalculateLayout()
    {
        // Force rebuild layout immediately
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentLayout.GetComponent<RectTransform>());
    }
}
