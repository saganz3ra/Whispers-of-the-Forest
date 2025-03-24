using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string customMessage;

    public string GetInteractionMessage()
    {
        return string.IsNullOrEmpty(customMessage) ? "Interagir com " + gameObject.name : customMessage;
    }

    public virtual void Interact() // Agora pode ser sobrescrito
    {
        Debug.Log($"Interagiu com {gameObject.name}");
    }
}
