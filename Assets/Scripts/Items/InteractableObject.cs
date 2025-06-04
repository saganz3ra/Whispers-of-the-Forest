using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public void Interact()
    {
        Quest current = QuestManager.Instance.GetCurrentQuest();

        if (current != null && !current.isCompleted &&
            gameObject.CompareTag(current.goal.targetObjectTag))
        {
            current.goal.CompleteGoal();
            QuestManager.Instance.CompleteCurrentQuest();
            Debug.Log("Objetivo alcançado: " + gameObject.name);

            // Se for um item carregável, anexa ao player
            PickupAttach pickup = GetComponent<PickupAttach>();
            if (pickup != null)
            {
                Transform playerContainer = GameObject.FindGameObjectWithTag("Player").transform;
                Transform player = playerContainer.Find("InGame Player");
                pickup.AttachToPlayer(player);
            }
            else if (GetComponent<NonDestroyable>() == null)
            {
                // Só destrói se NÃO for marcado como não-destrutível
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.LogWarning("Você não pode interagir com isso ainda. Complete a missão anterior.");
        }
    }
}
