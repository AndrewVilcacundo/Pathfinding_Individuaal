using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPoint = 0;

    public float detectionRange = 8f;
    public Transform player;

    private NavMeshAgent agent;

    private enum State { Patrol, Chase }
    private State currentState = State.Patrol;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToNextPatrolPoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrol:
                PatrolBehavior(distanceToPlayer);
                break;

            case State.Chase:
                ChaseBehavior(distanceToPlayer);
                break;
        }
    }

    void PatrolBehavior(float distanceToPlayer)
    {
        if (distanceToPlayer < detectionRange)
        {
            currentState = State.Chase;
            return;
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }

    void ChaseBehavior(float distanceToPlayer)
    {
        if (distanceToPlayer > detectionRange * 1.5f)
        {
            currentState = State.Patrol;
            GoToNextPatrolPoint();
            return;
        }

        agent.SetDestination(player.position);
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.SetDestination(patrolPoints[currentPoint].position);

        currentPoint = (currentPoint + 1) % patrolPoints.Length;
    }
}
