using UnityEngine;
using UnityEngine.AI;

public class CustomerMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // ✅ UNIQUE fonction de déplacement
    public void MoveTo(Transform target)
    {
        if (target == null) return;

        agent.SetDestination(target.position);
    }

    public bool HasReached()
    {
        return !agent.pathPending && agent.remainingDistance < 0.6f;
    }

    // ✅ remplacement propre de "Stop"
    public void StopMoving()
    {
        agent.ResetPath();
    }
}