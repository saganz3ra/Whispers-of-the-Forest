using UnityEngine;

public class Door : ObjectiveItem
{
    public Animator doorAnim;

    // RequiredObjective = 1
    protected override void OnInteractSuccess()
    {
        base.OnInteractSuccess();
        doorAnim.SetTrigger("Open");
    }
}
