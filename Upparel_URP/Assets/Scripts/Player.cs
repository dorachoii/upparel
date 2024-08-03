using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public Counter selectedCounter;
    }
    //SerializeField를 하면 private이어도 보이게 할 수 있음!
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private bool isWalking;

    //물체 앞에 멈춰 서 있어도 확인 가능하게끔
    private Vector3 lastInteractionDir;

    private Counter selectedCounter;

    private void Awake()
    {
        //플레이어가 하나 이상이면 안됨, 싱글턴 패턴이기 때문에
        if( Instance != null)
        {
            Debug.LogError("Therer is more than one Player instance.");
        }

        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
        }

    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();

    }

    //물건과 상호작용하는 것을 도와주는 코드
    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractionDir = moveDir;
        }
        //물건과 상호작용하기 위한 최소 거리
        float interactionDistance = 2f;

        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactionDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out Counter clearCounter))
            {
                //Counter이라는 스크립트를 가지고 있다면
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);

                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        // 한번 눌릴 때마다 얼마나 움직일 것인지
        float moveDistance = moveSpeed * Time.deltaTime;

        // 플레이어의 크기가 얼마나 되는지
        float playerRadius = 0.7f;

        // 플레이어의 키가 얼마나 되는지 
        float playerHeight = 2f;

        //Physics를 이용해 움직일 수 있는지 확인하는 bool값
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        // capsuleCast에 걸리는 게 없다면 움직이게끔 하라!
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        //움직일 수 없다면, 사선으로 움직이는 것은 허용되는 지 확인
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //Can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    //Can move only on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot move in any direction
                }

            }

        }

        //움직이려고 하는 방향이 있다면 isWalking을 참으로 바꾼
        isWalking = moveDir != Vector3.zero;

        //이동할 때 내가 원하는 방향 보게끔 해주는
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        //Debug.Log(inputVector);

    }

    // isWalking이라는 bool값을 return
    public bool IsWalking()
    {
        return isWalking;
    }


    private void SetSelectedCounter(Counter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}
