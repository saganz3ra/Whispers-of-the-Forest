using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : States
{
    public Transform player;
    public float detectionRange = 10f;
    public ChaseState chaseState;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override States RunCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            return chaseState;
        }

        // Pode adicionar patrulha futura aqui
        return this;
    }
}
