using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorials : MonoBehaviour
{
     public CanvasGroup canvasGroup; // CanvasGroup을 연결합니다.
     public GameObject tooltip;
    public float fadeDuration = 2.0f; // 페이드 아웃 시간

    void Start()
    {
        tooltip.SetActive(true);
        // 페이드 아웃 시작
        StartCoroutine(FadeOutAndDisable());
    }

    IEnumerator FadeOutAndDisable()
    {
        float startAlpha = canvasGroup.alpha; // 현재 알파값을 가져옵니다.
        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress); // 알파값을 서서히 줄입니다.
            progress += rate * Time.deltaTime;

            yield return null; // 다음 프레임까지 대기합니다.
        }

        canvasGroup.alpha = 0; // 최종적으로 알파값을 0으로 설정
        canvasGroup.gameObject.SetActive(false); // Canvas 비활성화
    }
}
