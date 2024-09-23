using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movable : MonoBehaviour
{
    NavMeshAgent agent;
    private Animator animator; // 애니메이터를 사용할 변수 추가
    
    List<Vector3> PatrolPos = new List<Vector3>
    {
        new Vector3(0, 0, 0),
        new Vector3(25, 2.5f, 3.5f),
        new Vector3(-0.5f, -0.5f, 30),
        new Vector3(-23, -0.5f, 20),
        new Vector3(-30, -0.5f, 1.8f),
        new Vector3(-4, -0.5f, -16)
    };

    Vector3 waypointTarget;  
    private Vector3 danceTarget;  // 목표 지점
    public float waypointReachedThreshold = 1.0f;  // 웨이포인트 도달 거리
    private bool isMovingToDanceLocation = false;  // Dance 위치로 이동 중인지 확인하는 플래그
    private bool isPatrolling = true; // 패트롤 상태를 관리하는 플래그

    private float originalSpeed; // 원래 NavMeshAgent의 속도를 저장

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Animator 컴포넌트를 가져옵니다.
        originalSpeed = agent.speed; // 원래 속도를 저장합니다.
    }

    void Start()
    {
        StartPatrol();
    }

    void Update()
    {
        // 목표 지점에 도달했는지 확인
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (isMovingToDanceLocation)
            {
                // Dance 위치에 도달했으면 애니메이션 실행
                PlayDanceAnimation();
                isMovingToDanceLocation = false; // 플래그를 해제
                agent.speed = originalSpeed; // 속도를 원래대로 복원
                StartCoroutine(ResumePatrolAfterDance()); // 일정 시간 후에 패트롤 재개
            }
            else if (isPatrolling)
            {
                // 다음 웨이포인트로 이동
                GotoNextWaypoint();
            }
        }
        
        // Dance 위치로 이동 중일 때 1m 이내로 가까워졌는지 확인
        if (isMovingToDanceLocation && Vector3.Distance(transform.position, danceTarget) <= 1f)
        {
            PlayDanceAnimation();
            isMovingToDanceLocation = false;
            agent.speed = originalSpeed; // 속도를 원래대로 복원
            StartCoroutine(ResumePatrolAfterDance()); // 일정 시간 후에 패트롤 재개
        }
    }

    public void StartPatrol()
    {
        isPatrolling = true;
        GotoNextWaypoint();
    }

    public void StopPatrol()
    {
        isPatrolling = false;
        agent.ResetPath(); // 현재 경로를 멈춤
    }

    public void GotoNextWaypoint()
    {
        if (!isPatrolling) return; // 패트롤 중이 아니면 리턴

        int randomIndex = Random.Range(0, PatrolPos.Count);
        waypointTarget = PatrolPos[randomIndex];

        agent.SetDestination(waypointTarget);
    }

    // 특정 위치로 이동하는 함수
    public void MoveToTarget(Vector3 targetPosition)
{
    StopPatrol(); // 패트롤을 멈추고 지정된 위치로 이동
    agent.speed = 10f; // 속도를 10으로 설정
    agent.SetDestination(targetPosition);
    
    danceTarget = targetPosition;
    isMovingToDanceLocation = true; // Dance 위치로 이동 중임을 표시
}


    // Dance 애니메이션을 실행하는 함수
    public void PlayDanceAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Dance");
        }
    }

    // 일정 시간 후에 패트롤 재개
    private IEnumerator ResumePatrolAfterDance()
    {
        yield return new WaitForSeconds(3f); // 3초 동안 Dance 애니메이션 실행
        StartPatrol();
    }
}
