using UnityEngine;

public class ObjectiveItemDestroyDoor : ObjectiveItem
{
    [Header("Refer�ncia � porta")]
    public GameObject doorToRemove;

    [Header("Slot do item que ser� destru�do (ex: p� de cabra)")]
    public string carriedItemSlot = "CrowbarSlot";

    public override void Interact()
    {
        if (!ObjectiveManager.Instance.IsObjective(requiredObjective))
        {
            MessageUI.Instance?.ShowMessage(blockedMessage);
            return;
        }

        // Remove a porta
        if (doorToRemove != null)
        {
            doorToRemove.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Porta para remover n�o foi atribu�da.");
        }

        // Remove o item da m�o
        GameObject slot = GameObject.Find(carriedItemSlot);
        if (slot != null && slot.transform.childCount > 0)
        {
            Transform item = slot.transform.GetChild(0);
            Destroy(item.gameObject);
        }

        MessageUI.Instance?.ShowMessage(successMessage);

        if (advanceOnUse)
            ObjectiveManager.Instance.AdvanceObjective();

        // Desabilita o colisor ou o script ap�s o uso
        GetComponent<Collider>().enabled = false;
        this.enabled = false;
    }
}
