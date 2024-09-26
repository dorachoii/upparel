using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitWantedSec : MonoBehaviour
{
    public UnityEvent OnCalled;
    public UnityEvent OnTween;
    public float Sec;
    
    public RectTransform popupBadge; // popup_badge의 RectTransform
    public CanvasGroup cg;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void call()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(Sec);

        OnCalled.Invoke(); 

        StartCoroutine(wait2()); // wait2 코루틴을 여기서 호출
    }

    IEnumerator wait2()
{
    yield return new WaitForSeconds(3.5f);
    print("wait2실행!");

    float duration = 1.5f;
    float elapsedTime = 0f;

    Vector2 startPos = popupBadge.anchoredPosition; // 시작 위치
    Vector2 endPos = new Vector2(startPos.x, -2000); // 목표 위치
    float startAlpha = cg.alpha; // 시작 알파 값

    while (elapsedTime < duration)
    {
        // 위치 이동
        popupBadge.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / duration);
        
        // 알파 값 감소
        cg.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / duration);

        elapsedTime += Time.deltaTime;
        yield return null; // 다음 프레임까지 대기
    }

    // 최종 위치 및 알파 값 설정 (오차 방지를 위해 마지막에 값 고정)
    popupBadge.anchoredPosition = endPos;
    cg.alpha = 0f;

    OnTween.Invoke();
}

}
