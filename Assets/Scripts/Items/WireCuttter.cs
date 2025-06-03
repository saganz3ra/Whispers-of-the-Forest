public class WireCutter : ObjectiveItem
{
    // RequiredObjective = 5
    protected override void OnInteractSuccess()
    {
        base.OnInteractSuccess();
        gameObject.SetActive(false);
    }
}
