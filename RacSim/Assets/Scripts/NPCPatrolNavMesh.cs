using UnityEngine;
using UnityEngine.AI;

public class NPCPatrolNavMesh : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float waitTimeAtPoint = 1f;
    public Transform body;
    public int startingPointIndex = 0;
    public float rotationSpeed = 8f;

    private NavMeshAgent agent;
    private int currentPointIndex;
    private float waitTimer = 0f;
    private bool isWaiting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        if (body == null)
            body = transform;

        if (patrolPoints == null || patrolPoints.Length == 0)
            return;

        currentPointIndex = Mathf.Clamp(startingPointIndex, 0, patrolPoints.Length - 1);
        agent.SetDestination(patrolPoints[currentPointIndex].position);
    }

    void Update()
    {
        if (patrolPoints == null || patrolPoints.Length == 0 || agent == null)
            return;

        if (!isWaiting)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.05f)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f)
                {
                    isWaiting = true;
                    waitTimer = waitTimeAtPoint;
                }
            }
        }
        else
        {
            waitTimer -= Time.deltaTime;

            if (waitTimer <= 0f)
            {
                currentPointIndex++;

                if (currentPointIndex >= patrolPoints.Length)
                    currentPointIndex = 0;

                agent.SetDestination(patrolPoints[currentPointIndex].position);
                isWaiting = false;
            }
        }

        RotateBodyTowardMovement();
    }

    void RotateBodyTowardMovement()
    {
        if (body == null) return;

        Vector3 velocity = agent.velocity;
        velocity.y = 0f;

        if (velocity.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}