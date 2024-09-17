using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource_BG;
    public AudioSource audioSource_SFX;

    public static SoundManager instance;

    void Awake()
    {
        // 기존에 있는 SoundManager 인스턴스가 없다면 이 인스턴스를 사용
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 오브젝트 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 기존에 SoundManager가 있으면 새로운 오브젝트 파괴
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (audioClips.Length > 0 && audioSource_BG != null)
        {
            audioSource_BG.clip = audioClips[3];  // 0번째 오디오 클립 설정
            audioSource_BG.Play();  // 오디오 클립 재생
        }
        else
        {
            Debug.LogWarning("AudioClips array is empty or AudioSource_BG is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 필요 시 Update에서 다른 기능 구현 가능
    }
}
