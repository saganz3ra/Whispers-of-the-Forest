public class Rope : ObjectiveItem
{
    // RequiredObjective = 7
    protected override void OnInteractSuccess()
    {
        base.OnInteractSuccess();
        gameObject.SetActive(false);
    }
}
