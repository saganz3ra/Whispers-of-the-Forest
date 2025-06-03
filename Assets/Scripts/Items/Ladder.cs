using UnityEngine;

public class Ladder : ObjectiveItem
{
    public Transform targetPosition;

    // RequiredObjective = 4
    protected override void OnInteractSuccess()
    {
        base.OnInteractSuccess();
        transform.position = targetPosition.position;
        transform.rotation = targetPosition.rotation;
    }
}
