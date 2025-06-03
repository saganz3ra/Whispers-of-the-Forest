public class Axe : ObjectiveItem
{
    // RequiredObjective = 2
    protected override void OnInteractSuccess()
    {
        base.OnInteractSuccess();
        gameObject.SetActive(false);
    }
}
