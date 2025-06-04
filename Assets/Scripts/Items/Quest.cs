using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    [TextArea] public string description;
    public bool isCompleted;
    public QuestGoal goal;
}