using UnityEngine;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text descriptionText;

    void Update()
    {
        Quest current = QuestManager.Instance.GetCurrentQuest();
        if (current != null)
        {
            titleText.text = current.questName;
            descriptionText.text = current.description;
        }
    }
}
