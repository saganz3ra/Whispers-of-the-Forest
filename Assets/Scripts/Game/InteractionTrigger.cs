using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    private bool playerInRange = false;
    private ObjectiveItem objectiveItem;

    void Start()
    {
        objectiveItem = GetComponent<ObjectiveItem>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (objectiveItem != null)
            {
                objectiveItem.Interact();
            }
        }
    }
}
