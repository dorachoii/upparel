using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clean : MonoBehaviour
{
    public GameObject pollution;
    public GameObject flower;
    public GameObject cleaningFX;
    public Camera cam_pollution;

    private Movable[] allMovableObjects; // 모든 Movable 오브젝트를 저장할 배열
    public float radius = 3f; // 캐릭터들이 원을 그릴 반지름

    void Start()
    {
        // 씬 내에 있는 모든 Movable 오브젝트를 찾습니다.
        allMovableObjects = FindObjectsOfType<Movable>();
    }

    public void clean()
    {
        cam_pollution.GetComponent<Camera>().enabled = true;

        // 캐릭터들을 원형으로 배치
        int numberOfCharacters = allMovableObjects.Length;
        for (int i = 0; i < numberOfCharacters; i++)
        {
            // 각도를 계산하여 각 캐릭터의 목표 위치를 설정
            float angle = i * Mathf.PI * 2 / numberOfCharacters; // 360도 / 캐릭터 수
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            Vector3 targetPosition = cleaningFX.transform.position + offset; // pollution을 중심으로 반지름만큼 떨어진 위치

            // 각 캐릭터를 해당 위치로 이동
            allMovableObjects[i].MoveToTarget(targetPosition);
        }

        StartCoroutine(ExecuteAfterDelay());
    }

    IEnumerator ExecuteAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        AudioClip clean = SoundManager.instance.audioClips[1];
        SoundManager.instance.audioSource_SFX.PlayOneShot(clean);
        
        cleaningFX.SetActive(true);
        pollution.SetActive(false);
        flower.SetActive(true);
    }
}
