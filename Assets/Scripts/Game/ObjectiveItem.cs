using UnityEngine;

public class ObjectiveItem : MonoBehaviour
{
    public int requiredObjective;
    public string blockedMessage = "Ainda não posso usar isso.";
    public string successMessage = "Item utilizado.";
    public bool advanceOnUse = true;

    public virtual void Interact()
    {
        if (!ObjectiveManager.Instance.IsObjective(requiredObjective))
        {
            MessageUI.Instance.ShowMessage(blockedMessage);
            return;
        }

        OnInteractSuccess();

        if (advanceOnUse)
            ObjectiveManager.Instance.AdvanceObjective();
    }

    protected virtual void OnInteractSuccess()
    {
        MessageUI.Instance.ShowMessage(successMessage);
    }
}
