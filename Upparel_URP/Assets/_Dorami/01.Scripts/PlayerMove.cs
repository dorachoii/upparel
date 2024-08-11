using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance;

    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    protected CharacterController cc;
    protected PlayerActionsExample playerInput;
    
    private Vector3 playerVelocity;
    private bool isGrounded;

    public Animator animator;
    public float rotationSpeed = 1.0f; // 회전 속도

    void Awake()
    {
        Instance = this;
        cc = GetComponent<CharacterController>();
        playerInput = new PlayerActionsExample();
    }

    void Update()
    {
        isGrounded = cc.isGrounded;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        Vector2 movement = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);

        if (move == Vector3.zero)
        {
            animator.SetTrigger("WalkToIdle");
        }
        else
        {
            cc.Move(move * Time.deltaTime * playerSpeed);

            gameObject.transform.forward = -move;
            animator.SetTrigger("IdleToWalk");
        }

        bool jumpPress = playerInput.Player.Jump.triggered;
        if (jumpPress && isGrounded)
        {
            // animator.SetTrigger("IdleToJump");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        cc.Move(playerVelocity * Time.deltaTime);
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    public void think()
    {
        animator.SetTrigger("IdleToThink");
        PlayerCamera.instance.ZoomIn();
        StartCoroutine(SmoothLookAt(Camera.main.transform, 1f));
    }

    public void idle()
    {
        animator.SetTrigger("ThinkToIdle");
        PlayerCamera.instance.ZoomOut();
    }

    private IEnumerator SmoothLookAt(Transform target, float duration)
{
    Quaternion initialRotation = transform.rotation;

    // target의 위치와 현재 오브젝트의 위치를 이용해 방향을 계산
    Vector3 direction = target.position - transform.position;

    // y축 회전을 유지하기 위해 x, z 값을 0으로 고정
    direction.y = 0;

    // LookRotation을 통해 새로운 타겟 회전을 계산
    Quaternion targetRotation = Quaternion.LookRotation(-direction);

    float elapsedTime = 0f;

    while (elapsedTime < duration)
    {
        // initialRotation에서 targetRotation으로의 부드러운 회전 (y축만 회전)
        transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    // 최종적으로 y축 회전을 targetRotation으로 설정
    transform.rotation = targetRotation;
}

}
