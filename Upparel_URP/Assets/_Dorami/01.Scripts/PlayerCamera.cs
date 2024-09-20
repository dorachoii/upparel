using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public Transform player; // 로컬 플레이어의 트랜스폼

    private Vector3 initialOffset;  // 카메라와 플레이어 사이의 초기 오프셋
    private Vector3 zoomedInOffset = new Vector3(0.1f, 1f, -3.5f); // 줌인시 사용할 오프셋
    private Vector3 currentOffset; // 현재 오프셋

    private Quaternion initialRotation; // 초기 회전값
    private Quaternion zoomedInRotation = Quaternion.Euler(0, 35, 0); // 줌인시 사용할 회전값
    private Quaternion currentRotation; // 현재 회전값

    private bool isZooming = false; // 줌 중인지 여부를 나타내는 플래그

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (player != null)
        {
            initialOffset = transform.position - player.position;
            initialRotation = transform.rotation;

            currentOffset = initialOffset;
            currentRotation = initialRotation;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // 플레이어를 따라 카메라 이동 및 회전
            transform.position = player.position + currentOffset;
            transform.rotation = currentRotation;
        }
    }

    // 카메라가 특정 플레이어를 팔로우하도록 설정하는 함수
    public void FollowPlayer(Transform playerTransform)
    {
        player = playerTransform;
        initialOffset = transform.position - player.position;
        initialRotation = transform.rotation;
        currentOffset = initialOffset;
        currentRotation = initialRotation;
    }

    public void ZoomIn()
    {
        if (!isZooming)
        {
            StartCoroutine(SmoothTransition(zoomedInOffset, zoomedInRotation, 1f));
        }
    }

    public void ZoomOut()
    {
        if (!isZooming)
        {
            StartCoroutine(SmoothTransition(initialOffset, initialRotation, 1f));
        }
    }

    private IEnumerator SmoothTransition(Vector3 targetOffset, Quaternion targetRotation, float duration)
    {
        isZooming = true;
        Vector3 startOffset = currentOffset;
        Quaternion startRotation = currentRotation;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            currentOffset = Vector3.Lerp(startOffset, targetOffset, elapsedTime / duration);
            currentRotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentOffset = targetOffset;
        currentRotation = targetRotation;
        isZooming = false;
    }
}
