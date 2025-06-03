using TMPro;
using UnityEngine;

public class MessageUI : MonoBehaviour
{
    public static MessageUI Instance;
    public TextMeshProUGUI messageText;
    public float messageDuration = 2f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("J� existe uma inst�ncia de MessageUI! Destruindo duplicata.");
            Destroy(gameObject);
        }
    }

    public void ShowMessage(string message)
    {
        if (messageText == null)
        {
            Debug.LogError("Message Text n�o foi atribu�do no Inspector!");
            return;
        }

        StopAllCoroutines();
        StartCoroutine(Show(message));
    }

    private System.Collections.IEnumerator Show(string message)
    {
        messageText.text = message;
        messageText.enabled = true;
        yield return new WaitForSeconds(messageDuration);
        messageText.enabled = false;
    }
}
