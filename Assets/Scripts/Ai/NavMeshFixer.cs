using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshFixer : MonoBehaviour
{
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        NavMeshHit hit;

        // Procura por uma NavMesh pr�xima at� 5 unidades de dist�ncia
        if (NavMesh.SamplePosition(transform.position, out hit, 5f, NavMesh.AllAreas))
        {
            transform.position = hit.position; // Reposiciona para a NavMesh
            Debug.Log("Inimigo reposicionado para NavMesh.");
        }
        else
        {
            Debug.LogWarning("Nenhuma NavMesh pr�xima encontrada para reposicionar o inimigo.");
        }
    }
}
