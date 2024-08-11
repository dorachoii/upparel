using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public Transform player; // 플레이어의 트랜스폼을 지정

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
        // 초기 오프셋 및 회전값 계산 및 설정
        initialOffset = transform.position - player.position;
        initialRotation = transform.rotation;

        currentOffset = initialOffset;
        currentRotation = initialRotation;

        print("지금 오프셋은" + currentOffset);
    }

    void LateUpdate()
    {
        // 플레이어 팔로우캠
        transform.position = player.position + currentOffset;
        transform.rotation = currentRotation;
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
        Vector3 startOffset = currentOffset; // 현재 오프셋 저장
        Quaternion startRotation = currentRotation; // 현재 회전값 저장
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // 오프셋과 회전값을 선형 보간하여 카메라를 부드럽게 이동 및 회전
            currentOffset = Vector3.Lerp(startOffset, targetOffset, elapsedTime / duration);
            currentRotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종 오프셋 및 회전값을 설정
        currentOffset = targetOffset;
        currentRotation = targetRotation;
        isZooming = false;
    }
}
