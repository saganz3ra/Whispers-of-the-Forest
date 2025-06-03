using  UnityEngine;

public class WallEscape : ObjectiveItem
{
    // RequiredObjective = 8
    protected override void OnInteractSuccess()
    {
        base.OnInteractSuccess();
        Debug.Log("Você escapou!");
        // cena de finalização
    }
}
