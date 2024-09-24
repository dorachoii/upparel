// using System.Collections;
// using UnityEngine;

// public class PollutionCamera : MonoBehaviour
// {
//     public static PollutionCamera instance;
//     private GameObject player; // Reference to the player object

//     private Vector3 initialOffset;  // Initial offset between the camera and player
//     private Vector3 zoomedInOffset = new Vector3(0f, 2f, -4.5f); // Offset for zoom-in
//     private Vector3 currentOffset; // Current offset

//     private Quaternion initialRotation; // Initial rotation
//     private Quaternion zoomedInRotation = Quaternion.Euler(10f, 0f, 0f); // Rotation for zoom-in
//     private Quaternion currentRotation; // Current rotation

//     private bool isZooming = false; // Flag to indicate if zooming is in progress

//     void Awake()
//     {
//         instance = this;
//     }

//     void Start()
//     {
//         player = GameObject.FindWithTag("PlayerTag");

//         if (player != null)
//         {
//             initialOffset = transform.position - player.transform.position;
//             initialRotation = transform.rotation;

//             currentOffset = initialOffset;
//             currentRotation = initialRotation;
//         }
//     }

//     void LateUpdate()
//     {
//         if (player != null)
//         {
//             // Update the camera's position and rotation to follow the player
//             transform.position = player.transform.position + currentOffset;
//             transform.rotation = currentRotation;
//         }
//     }

//     // Function to make the camera follow a specific player
//     public void FollowPlayer(GameObject playerObject)
//     {
//         player = playerObject;
//         initialOffset = transform.position - player.transform.position;
//         initialRotation = transform.rotation;
//         currentOffset = initialOffset;
//         currentRotation = initialRotation;
//     }

//     public void ZoomIn()
//     {
//         if (!isZooming)
//         {
//             StartCoroutine(SmoothTransition(zoomedInOffset, zoomedInRotation, 1f));
//         }
//     }

//     public void ZoomOut()
//     {
//         if (!isZooming)
//         {
//             StartCoroutine(SmoothTransition(initialOffset, initialRotation, 1f));
//         }
//     }

//     private IEnumerator SmoothTransition(Vector3 targetOffset, Quaternion targetRotation, float duration)
//     {
//         isZooming = true;
//         Vector3 startOffset = currentOffset;
//         Quaternion startRotation = currentRotation;
//         float elapsedTime = 0f;

//         while (elapsedTime < duration)
//         {
//             currentOffset = Vector3.Lerp(startOffset, targetOffset, elapsedTime / duration);
//             currentRotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
//             elapsedTime += Time.deltaTime;
//             yield return null;
//         }

//         currentOffset = targetOffset;
//         currentRotation = targetRotation;
//         isZooming = false;
//     }
// }
