using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager_World : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject); // 이 오브젝트가 씬 간에 파괴되지 않도록 설정
    }

    public void moveToCollectScene()
    {
        SceneManager.LoadScene(3);
    }

    public void backToWorldScene()
    {
        SceneManager.LoadScene(2);
    }
}
