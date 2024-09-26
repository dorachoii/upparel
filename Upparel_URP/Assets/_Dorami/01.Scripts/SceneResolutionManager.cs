using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneResolutionManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject); // 이 오브젝트가 씬 간에 파괴되지 않도록 설정
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬이 로드될 때 호출되는 이벤트에 등록
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬의 이름 또는 인덱스에 따라 적절한 해상도를 설정
        switch (scene.buildIndex)
        {
            case 0: // 씬 인덱스 0번 (세로 해상도)
                Screen.SetResolution(1179, 2556, false); // 세로 해상도로 설정
                break;
            case 1: // 씬 인덱스 1번 (가로 해상도)
            case 2: // 씬 인덱스 2번 (가로 해상도)
                Screen.SetResolution(2556, 1179, false); // 가로 해상도로 설정
                break;
            case 3:
                Screen.SetResolution(1179, 2556, false); // 세로 해상도로 설정
                break;
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 메모리 누수를 방지하기 위해 이벤트 등록 해제
    }
}
