using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movable : MonoBehaviour
{
    NavMeshAgent agent;
    private Animator animator; // Animator variable
    private GameObject player; // Reference to the player object
    
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
    private Vector3 danceTarget;  // Target position
    public float waypointReachedThreshold = 1.0f;  // Waypoint reach distance
    private bool isMovingToDanceLocation = false;  // Flag to check if moving to dance location
    private bool isPatrolling = true; // Flag to manage patrolling state

    private float originalSpeed; // Store original NavMeshAgent speed
    private float avoidanceRadius = 1f; // Avoidance distance from PlayerTag objects

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Get Animator component
        originalSpeed = agent.speed; // Store original speed
    }

    void Start()
    {
        StartPatrol();
    }

    void Update()
    {
        // Check if the agent reached the target
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (isMovingToDanceLocation)
            {
                // Play dance animation
                PlayDanceAnimation();
                isMovingToDanceLocation = false; // Reset flag
                agent.speed = originalSpeed; // Restore original speed
                StartCoroutine(ResumePatrolAfterDance()); // Resume patrol after dancing
            }
            else if (isPatrolling)
            {
                // Move to the next waypoint
                GotoNextWaypoint();
            }
        }

        // Check if moving to the dance location and within 1 meter
        if (isMovingToDanceLocation && Vector3.Distance(transform.position, danceTarget) <= 1f)
        {
            PlayDanceAnimation();
            isMovingToDanceLocation = false;
            agent.speed = originalSpeed; // Restore original speed
            StartCoroutine(ResumePatrolAfterDance()); // Resume patrol after dancing
        }

        // Check for nearby PlayerTag objects and avoid them
        AvoidPlayerObjects();
    }

    public void StartPatrol()
    {
        isPatrolling = true;
        GotoNextWaypoint();
    }

    public void StopPatrol()
    {
        isPatrolling = false;
        agent.ResetPath(); // Stop current path
    }

    public void GotoNextWaypoint()
    {
        if (!isPatrolling) return; // Return if not patrolling

        int randomIndex = Random.Range(0, PatrolPos.Count);
        waypointTarget = PatrolPos[randomIndex];

        agent.SetDestination(waypointTarget);
    }

    // Move to a specific target position
    public void MoveToTarget(Vector3 targetPosition)
    {
        StopPatrol(); // Stop patrolling and move to specified location
        agent.speed = 10f; // Set speed to 10
        agent.SetDestination(targetPosition);
        
        danceTarget = targetPosition;
        isMovingToDanceLocation = true; // Set flag to indicate moving to dance location
    }

    // Play dance animation
    public void PlayDanceAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Dance");
        }
    }

    // Resume patrol after a delay
    private IEnumerator ResumePatrolAfterDance()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        StartPatrol();
    }

    // Check for nearby PlayerTag objects and avoid them
    private void AvoidPlayerObjects()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerTag");
        foreach (GameObject playerObject in players)
        {
            float distance = Vector3.Distance(transform.position, playerObject.transform.position);
            if (distance <= avoidanceRadius)
            {
                Vector3 directionToAvoid = (transform.position - playerObject.transform.position).normalized;
                Vector3 newTargetPosition = transform.position + directionToAvoid * avoidanceRadius;

                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(newTargetPosition, path) && path.status == NavMeshPathStatus.PathComplete)
                {
                    agent.SetPath(path); // Set new path to avoid the player
                    break;
                }
            }
        }
    }
}
