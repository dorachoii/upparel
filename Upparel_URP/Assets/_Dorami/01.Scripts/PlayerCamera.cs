using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public Transform player; // Local player's transform

    private Vector3 initialOffset;  // Initial offset between camera and player
    private Vector3 zoomedInOffset = new Vector3(0.1f, 1f, -3.5f); // Offset for the first zoom-in
    private Vector3 zoomedInOffset2 = new Vector3(0f, 1f, -2.2f); // Offset for ZoomIn2
    private Vector3 currentOffset; // Current offset

    private Quaternion initialRotation; // Initial rotation
    private Quaternion zoomedInRotation = Quaternion.Euler(0, 35, 0); // Rotation for the first zoom-in
    private Quaternion zoomedInRotation2 = Quaternion.Euler(10f, 0f, 0f); // Rotation for ZoomIn2
    private Quaternion currentRotation; // Current rotation

    private bool isZooming = false; // Flag to indicate zooming status

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
            // Move and rotate camera to follow player
            transform.position = player.position + currentOffset;
            transform.rotation = currentRotation;
        }
    }

    // Function to make the camera follow a specific player
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

    public void ZoomIn2()
    {
        if (!isZooming)
        {
            StartCoroutine(SmoothTransition(zoomedInOffset2, zoomedInRotation2, 1f));
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
