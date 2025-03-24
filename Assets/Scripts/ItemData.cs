using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item Data")]

public class ItemData : ScriptableObject
{

    public string itemName;
    public string description;
    public GameObject itemModel;

}