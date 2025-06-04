using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public string targetObjectTag;
    public bool isAchieved;

    public bool IsReached()
    {
        return isAchieved;
    }

    public void CompleteGoal()
    {
        isAchieved = true;
    }
}

public enum GoalType
{
    Collect,
    Destroy,
    Place,
    Escape
}