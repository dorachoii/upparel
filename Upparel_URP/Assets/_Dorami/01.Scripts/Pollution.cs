using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clean : MonoBehaviour
{
    public GameObject pollution;
    public GameObject flower;
    public GameObject cleaningFX;
    public Camera cam_pollution;

    void Start()
    {
    }

    void Update()
    {
    }

    public void clean()
    {
        // 카메라를 먼저 켜고
        cam_pollution.GetComponent<Camera>().enabled = true;

        // 나머지 동작을 1초 후에 실행하도록 코루틴 호출
        StartCoroutine(ExecuteAfterDelay());
    }

    IEnumerator ExecuteAfterDelay()
    {
        // 1초 대기
        yield return new WaitForSeconds(1f);

        // 1초 후에 실행될 코드들
        AudioClip clean = SoundManager.instance.audioClips[1];
        SoundManager.instance.audioSource_SFX.PlayOneShot(clean);
        
        cleaningFX.SetActive(true);
        pollution.SetActive(false);
        flower.SetActive(true);
    }
}
