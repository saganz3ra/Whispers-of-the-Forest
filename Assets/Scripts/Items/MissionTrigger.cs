using UnityEngine;

public class MissionTrigger : MonoBehaviour
{
    private bool isPlayerNear = false;
    private InteractableObject interactable;

    void Start()
    {
        interactable = GetComponent<InteractableObject>();
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            interactable.Interact();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
