using UnityEngine;

public class Torre : Interactable
{
    private bool isOpen = false;
    public float openHeight = 2f; // Altura que a porta sobe ao abrir
    private Vector3 closedPosition;
    private Vector3 openPosition;

    private Quaternion closedRotation;
    private Quaternion openRotationQuat;

    public float openSpeed = 2f; // Velocidade de abertura/fechamento
    private Renderer doorRenderer;
    public Color openColor = new Color(0.72f, 0.52f, 0.28f); // Marrom mais claro

    void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + new Vector3(0, openHeight, 0);

        closedRotation = transform.rotation;
        openRotationQuat = Quaternion.Euler(90, 0, 0) * closedRotation; // Rotação de cabeça para baixo

        doorRenderer = GetComponent<Renderer>(); // Obtém o Renderer para mudar a cor
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
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;
        Quaternion targetRotation = isOpen ? openRotationQuat : closedRotation;

        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * openSpeed;
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = targetRotation;

        // Mudar a cor ao interagir
        if (doorRenderer != null)
        {
            doorRenderer.material.color = openColor;
        }
    }
}
