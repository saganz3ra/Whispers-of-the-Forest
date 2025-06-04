using UnityEngine;

public class PickupAttach : MonoBehaviour
{
    public string holderName; // ex: "CrowbarHolder"

    private void Start()
    {
        transform.SetParent(null); // Remove da hierarquia de pai
        DontDestroyOnLoad(gameObject);
    }

    public void AttachToPlayer(Transform player)
    {
        Transform holder = player.Find(holderName);
        if (holder != null)
        {
            // Resetar antes de parentear para evitar heran�a de escala
            transform.SetParent(null);
            transform.localScale = Vector3.one;

            // Agora parentear e ajustar
            transform.SetParent(holder);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one; // Herdar� a escala correta do holder

            // Garantir que seja vis�vel
            gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Holder n�o encontrado: " + holderName);
        }
    }

}
