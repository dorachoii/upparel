using UnityEngine;
using UnityEngine.SceneManagement;

public class ARButtonHandler : MonoBehaviour
{
    public void OnARButtonClick()
    {
        // Load the AR scene (replace "ARScene" with the actual name of your AR scene)
        SceneManager.LoadScene("ARSwatch");
    }
}
