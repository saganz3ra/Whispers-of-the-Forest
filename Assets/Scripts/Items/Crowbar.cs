public class Crowbar : ObjectiveItem
{
    //RequiredObjective = 0
    protected override void OnInteractSuccess()
    {
        base.OnInteractSuccess();
        gameObject.SetActive(false); // coleta o pé de cabra
    }
}