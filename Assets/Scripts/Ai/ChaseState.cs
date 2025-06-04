using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : States
{
    public Transform player;
    public float attackRange = 2f;
    public AttackState attackState;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override States RunCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            return attackState;
        }

        agent.SetDestination(player.position);
        return this;
    }
}
