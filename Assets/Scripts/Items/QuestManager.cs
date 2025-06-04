using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest[] quests;
    private int currentQuestIndex = 0;
    public static QuestManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (quests.Length > 0)
        {
            ActivateQuest(0);
        }
    }

    public void ActivateQuest(int index)
    {
        if (index < quests.Length)
        {
            quests[index].isCompleted = false;
            currentQuestIndex = index;
            Debug.Log("Missão ativada: " + quests[index].questName);
        }
    }

    public void CompleteCurrentQuest()
    {
        if (currentQuestIndex < quests.Length)
        {
            quests[currentQuestIndex].isCompleted = true;

            // ✅ Toca o som de missão completa
            AudioManagerInGame.Instance.PlayQuestComplete();

            currentQuestIndex++;

            if (currentQuestIndex >= quests.Length)
            {
                Debug.Log("Todas as missões concluídas!");

                EndGameUI ui = FindObjectOfType<EndGameUI>();
                if (ui != null)
                {
                    ui.ShowEndScreen();
                }
            }
            else
            {
                ActivateQuest(currentQuestIndex);
            }
        }
    }

    public Quest GetCurrentQuest()
    {
        if (currentQuestIndex < quests.Length)
            return quests[currentQuestIndex];
        else
            return null;
    }
}
