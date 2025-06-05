using UnityEngine;
using UnityEngine.AI;

public class AttackState : States
{
    public Transform player;
    public float stopAttackingDistance = 3f;
    public ChaseState chaseState;
    public float attackCooldown = 1.5f;
    public int damage = 50;

    private NavMeshAgent agent;
    private float lastAttackTime;
    private Animator animator;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    public override States RunCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stopAttackingDistance)
        {
            if (animator != null && HasParameter("isWalking"))
            {
                animator.SetBool("isWalking", true);
            }

            agent.isStopped = false;
            return chaseState;
        }

        agent.SetDestination(transform.position);
        agent.isStopped = true;

        if (animator != null && HasParameter("isWalking"))
        {
            Debug.Log("AttackState: Parando caminhada");
            animator.SetBool("isWalking", false);
        }

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }

        return this;
    }

    private void Attack()
    {
        Debug.Log("Atacando jogador!");

        if (animator != null && HasParameter("Attack", AnimatorControllerParameterType.Trigger))
        {
            Debug.Log("Trigger 'Attack' ativado.");
            animator.SetTrigger("Attack");
        }
        else
        {
            Debug.LogWarning("Trigger 'Attack' não existe no Animator!");
        }

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private bool HasParameter(string name, AnimatorControllerParameterType? type = null)
    {
        if (animator == null) return false;

        foreach (var param in animator.parameters)
        {
            if (param.name == name && (type == null || param.type == type))
                return true;
        }

        return false;
    }
}
