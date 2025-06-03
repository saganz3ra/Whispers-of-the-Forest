using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public float interactDistance = 3f;

    private List<ObjectiveItem> nearbyItems = new List<ObjectiveItem>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ObjectiveItem closest = GetClosestItem();
            if (closest != null)
            {
                closest.Interact();
            }
        }
    }

    ObjectiveItem GetClosestItem()
    {
        ObjectiveItem closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (var item in nearbyItems)
        {
            if (item == null) continue;

            float dist = Vector3.Distance(transform.position, item.transform.position);
            if (dist < interactDistance && dist < closestDistance)
            {
                closest = item;
                closestDistance = dist;
            }
        }

        return closest;
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjectiveItem item = other.GetComponent<ObjectiveItem>();
        if (item != null && !nearbyItems.Contains(item))
        {
            nearbyItems.Add(item);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ObjectiveItem item = other.GetComponent<ObjectiveItem>();
        if (item != null && nearbyItems.Contains(item))
        {
            nearbyItems.Remove(item);
        }
    }
}
