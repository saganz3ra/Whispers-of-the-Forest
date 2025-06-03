using UnityEngine;

public class Tree : ObjectiveItem
{
    public GameObject fallenTree;

    // RequiredObjective = 3
    protected override void OnInteractSuccess()
    {
        base.OnInteractSuccess();
        fallenTree.SetActive(true);
        gameObject.SetActive(false);
    }
}
