public class Fence : ObjectiveItem
{
    // RequiredObjective = 6
    protected override void OnInteractSuccess()
    {
        base.OnInteractSuccess();
        gameObject.SetActive(false); // ou anima��o de corte
    }
}
