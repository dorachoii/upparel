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

        // 떨어지지 않기 위해.. 콜라이더가 있는데도 필요한 코드인가?
        // if (isGrounded && playerVelocity.y < 0)
        // {
        //     playerVelocity.y = 0;
        // }

        Vector2 movement = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);

        if (move == Vector3.zero)
        {
            animator.SetTrigger("WalkToIdle");
        }
        else
        {
            cc.Move(move * Time.deltaTime * playerSpeed);

            gameObject.transform.forward = move;
            animator.SetTrigger("IdleToWalk");
        }

        bool jumpPress = playerInput.Player.Jump.triggered;
        if (jumpPress && isGrounded)
        {
            animator.SetTrigger("IdleToJump");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        cc.Move(playerVelocity * Time.deltaTime);
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
        Quaternion targetRotation = Quaternion.LookRotation( transform.position - target.position);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
