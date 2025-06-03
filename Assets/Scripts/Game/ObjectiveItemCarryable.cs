using UnityEngine;

public class ObjectiveItemCarryable : ObjectiveItem
{
    public bool destroyAfterUse = false;
    public bool carryItem = true;

    [Tooltip("Nome do GameObject vazio (slot) onde o item será posicionado, ex: CrowbarSlot, AxeSlot, etc.")]
    public string carrySlotName = "CrowbarSlot";

    public override void Interact()
    {
        if (!ObjectiveManager.Instance.IsObjective(requiredObjective))
        {
            if (MessageUI.Instance != null)
                MessageUI.Instance.ShowMessage(blockedMessage);
            return;
        }

        base.OnInteractSuccess(); // Mostra a mensagem de sucesso da base

        if (advanceOnUse)
            ObjectiveManager.Instance.AdvanceObjective();

        if (carryItem)
        {
            GameObject slot = GameObject.Find(carrySlotName);
            if (slot != null)
            {
                transform.SetParent(slot.transform);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                transform.localScale = Vector3.one;

                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null) rb.isKinematic = true;
                Collider col = GetComponent<Collider>();
                if (col != null) col.enabled = false;

                Debug.Log($"Item posicionado no slot '{carrySlotName}'.");
            }
            else
            {
                Debug.LogWarning($"Slot '{carrySlotName}' não encontrado na cena.");
            }
        }

        if (destroyAfterUse)
        {
            Destroy(gameObject);
        }
    }
}
