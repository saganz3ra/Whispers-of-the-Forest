using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : States
{
    public Transform player;
    public float stopAttackingDistance = 3f;
    public ChaseState chaseState;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override States RunCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stopAttackingDistance)
        {
            return chaseState;
        }

        agent.SetDestination(transform.position); // Para o inimigo
        // Aqui você pode adicionar a animação/ataque real
        Debug.Log("Atacando jogador!");
        return this;
    }
}

