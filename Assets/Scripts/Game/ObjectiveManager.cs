using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

    public int currentObjective = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AdvanceObjective()
    {
        currentObjective++;
        Debug.Log("Objetivo atualizado para: " + currentObjective);
    }

    public bool IsObjective(int id)
    {
        return currentObjective == id;
    }
}
