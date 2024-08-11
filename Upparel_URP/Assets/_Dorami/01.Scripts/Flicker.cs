using System.Collections;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public GameObject targetObject; // 플리커 효과를 적용할 게임 오브젝트
    public float flickerInterval = 0.5f; // 깜박이는 간격 (0.5초)

    void Start()
    {
        StartCoroutine(IFlicker());
    }

    IEnumerator IFlicker()
    {
        while (true)
        {
            // 현재 활성화 상태를 반전시킴
            targetObject.SetActive(!targetObject.activeSelf);
            // 지정된 시간만큼 대기
            yield return new WaitForSeconds(flickerInterval);
        }
    }
}
