using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // 씬 전환을 위해 추가

public class LoadingBar : MonoBehaviour
{
    public Slider slider;  // 슬라이더 연결
    public TextMeshProUGUI progressText;  // 텍스트 연결
    private float fillSpeed = 0.1f;  // 초당 증가량 (100% - 80%) / 2초 = 20% / 2초 = 0.1f
    private float targetValue = 1.0f;  // 최종 목표 값 (100%는 슬라이더의 max value가 1)
    private bool isSceneLoaded = false;  // 씬이 전환되었는지 확인하는 플래그

    void Start()
    {
        // 슬라이더를 80%로 초기화
        slider.value = 0.8f;
    }

    void Update()
    {
        if (slider.value < targetValue)
        {
            slider.value += fillSpeed * Time.deltaTime;
            progressText.text = Mathf.RoundToInt(slider.value * 100).ToString();  // 슬라이더 값을 퍼센트로 표시
        }
        else if (!isSceneLoaded)
        {
            isSceneLoaded = true;
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // 빌드 세팅의 2번 인덱스에 있는 씬으로 전환
        SceneManager.LoadScene(2);
    }
}
