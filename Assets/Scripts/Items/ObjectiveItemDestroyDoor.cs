using UnityEngine;

public class ObjectiveItemDestroyDoor : ObjectiveItem
{
    [Header("Referência à porta")]
    public GameObject doorToRemove;

    [Header("Slot do item que será destruído (ex: pé de cabra)")]
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
            Debug.LogWarning("Porta para remover não foi atribuída.");
        }

        // Remove o item da mão
        GameObject slot = GameObject.Find(carriedItemSlot);
        if (slot != null && slot.transform.childCount > 0)
        {
            Transform item = slot.transform.GetChild(0);
            Destroy(item.gameObject);
        }

        MessageUI.Instance?.ShowMessage(successMessage);

        if (advanceOnUse)
            ObjectiveManager.Instance.AdvanceObjective();

        // Desabilita o colisor ou o script após o uso
        GetComponent<Collider>().enabled = false;
        this.enabled = false;
    }
}
