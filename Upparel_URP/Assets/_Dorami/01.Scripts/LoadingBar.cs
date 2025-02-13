using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  

public class LoadingBar : MonoBehaviour
{
    public Slider slider; 
    public TextMeshProUGUI progressText; 
    private float fillSpeed = 0.1f;  
    private float targetValue = 1.0f;  
    private bool isSceneLoaded = false; 

    void Start()
    {
        slider.value = 0.8f;
    }

    void Update()
    {
        if (slider.value < targetValue)
        {
            slider.value += fillSpeed * Time.deltaTime;
            progressText.text = Mathf.RoundToInt(slider.value * 100).ToString();  
        }
        else if (!isSceneLoaded)
        {
            isSceneLoaded = true;
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(2);
    }

    
}

public class SceneManagerTutorial: MonoBehaviour
{
    public static SceneManagerTutorial instance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    // enum : constant를 가짐
    public enum Scene {
        MainMenu,   // 0
        Level01,    // 1
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene(Scene.Level01.ToString());
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }
}
