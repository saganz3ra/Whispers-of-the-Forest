using UnityEngine;
using UnityEngine.AI;

public class ChaseState : States
{
    public Transform player;
    public float attackRange = 2f;
    public AttackState attackState;

    private NavMeshAgent agent;
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public override States RunCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            if (animator != null && HasParameter("isWalking"))
            {
                animator.SetBool("isWalking", false); // Parar caminhada ao entrar em ataque
            }

            return attackState;
        }

        if (agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);

            if (animator != null && HasParameter("isWalking"))
            {
                animator.SetBool("isWalking", true); // Caminhada ativada ao perseguir
            }
        }
        else
        {
            Debug.LogWarning("NavMeshAgent não está na NavMesh!");
        }

        return this;
    }

    private bool HasParameter(string name)
    {
        if (animator == null) return false;

        foreach (var param in animator.parameters)
        {
            if (param.name == name)
                return true;
        }

        return false;
    }
}
