using UnityEngine;

public class Door : Interactable
{
    private bool isOpen = false;
    public float openRotation = 90f; // Ângulo de abertura da porta
    private Quaternion closedRotation;
    private Quaternion openRotationQuat;
    
    public float openSpeed = 2f; // Velocidade de abertura/fechamento

    void Start()
    {
        closedRotation = transform.rotation;
        openRotationQuat = Quaternion.Euler(0, openRotation, 0) * closedRotation;
    }

    public override void Interact()
    {
        base.Interact(); // Log padrão
        StopAllCoroutines();
        StartCoroutine(ToggleDoor());
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

        transform.rotation = targetRotation; // Garantir que chegue no alvo
    }
}
