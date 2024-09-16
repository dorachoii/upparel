using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class SceneManager2 : MonoBehaviour
{
    public GameObject menu;
    public GameObject loadingInterface;
    public GameObject loadingProgressBar;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    public void StartGame()
    {
        HideMenu();
        ShowLoadingScreen();
        // 백그라운드에서 불러올 수 있는?
        scenesToLoad.Add(SceneManager.LoadSceneAsync("GamePlay"));
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Level01Part01", LoadSceneMode.Additive));
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }

    public void ShowLoadingScreen()
    {
        loadingInterface.SetActive(true);
    }

    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;

        for(int i = 0; i < scenesToLoad.Count; ++i)
        {
            while(!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                loadingProgressBar.GetComponent<Image>().fillAmount = totalProgress/scenesToLoad.Count;
                yield return null;
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
