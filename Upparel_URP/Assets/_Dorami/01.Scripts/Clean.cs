using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clean : MonoBehaviour
{
    public GameObject pollution;
    public GameObject flower;
    public GameObject cleaningFX;
    public Camera cam_pollution;

    private Movable[] allMovableObjects; 
    public float radius = 3f; 

    public CanvasGroup canvasGroup;
    public CanvasGroup popup;

    float clearNum;

    PlayerState playerState;

    void Start()
    {
        allMovableObjects = FindObjectsOfType<Movable>();
    }

    public void clean()
    {
        cam_pollution.GetComponent<Camera>().enabled = true;

        int numberOfCharacters = allMovableObjects.Length;
        for (int i = 0; i < numberOfCharacters; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfCharacters; 
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            Vector3 targetPosition = cleaningFX.transform.position + offset; 

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

        yield return new WaitForSeconds(1f);
        

        while (canvasGroup.alpha > 0)
        {
            print("while문 실행중!");
            canvasGroup.alpha -= 0.1f * Time.deltaTime;
            popup.alpha  -= 0.1f * Time.deltaTime;
        }

        cleaningFX.SetActive(false);

        yield return new WaitForSeconds(1f);
        cam_pollution.GetComponent<Camera>().enabled = false;

        PlayerCamera.instance.ZoomOut();
        playerState.ChangeState(PlayerState.State.DANCE);

    }
}
