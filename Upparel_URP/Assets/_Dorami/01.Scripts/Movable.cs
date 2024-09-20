using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movable : MonoBehaviour
{
    NavMeshAgent agent;
    
    // 순회할 웨이포인트 리스트
    List<Vector3> PatrolPos = new List<Vector3>
    {
        new Vector3(0, 0, 0),
        new Vector3(25, 2.5f, 3.5f),
        new Vector3(-0.5f, -0.5f, 30),
        new Vector3(-23, -0.5f, 20),
        new Vector3(-30, -0.5f, 1.8f),
        new Vector3(-4, -0.5f, -16)
    };

    Vector3 waypointTarget;  // 현재 목표 웨이포인트
    public float waypointReachedThreshold = 1.0f;  // 웨이포인트 도달 거리

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartPatrol();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            GotoNextWaypoint();
        }
    }

    public void StartPatrol()
    {
        GotoNextWaypoint();
    }

    public void GotoNextWaypoint()
    {
        int randomIndex = Random.Range(0, PatrolPos.Count);
        waypointTarget = PatrolPos[randomIndex];

        agent.SetDestination(waypointTarget);
    }
}
