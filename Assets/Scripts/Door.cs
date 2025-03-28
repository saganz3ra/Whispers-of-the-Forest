using UnityEngine;

public class Door : Interactable
{
    private bool isOpen = false;
    public float openRotation = 90f; // Ângulo de abertura
    private Quaternion closedRotation;
    private Quaternion openRotationQuat;
    public float openSpeed = 2f; // Velocidade de abertura
    public ItemData requiredKey; // Chave necessária para abrir a porta

    void Start()
    {
        closedRotation = transform.rotation;
        openRotationQuat = Quaternion.Euler(0, openRotation, 0) * closedRotation;
    }

    public override void Interact()
    {
        base.Interact();

        if (requiredKey == null)
        {
            Debug.LogError("A chave necessária para abrir esta porta não foi definida!");
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Inventory inventory = player.GetComponent<Inventory>();
            if (inventory != null)
            {
                if (inventory.HasItem(requiredKey))
                {
                    StopAllCoroutines();
                    StartCoroutine(ToggleDoor());
                }
                else
                {
                    Debug.Log("Você precisa da chave correta para abrir esta porta!");
                }
            }
            else
            {
                Debug.LogError("O jogador não possui um componente Inventory!");
            }
        }
    }

    private System.Collections.IEnumerator ToggleDoor()
    {
        isOpen = !isOpen;
        Quaternion targetRotation = isOpen ? openRotationQuat : closedRotation;

        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
