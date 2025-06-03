using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerMessageUI : MonoBehaviour
{
    public static PlayerMessageUI Instance;

    public TextMeshProUGUI messageText;
    public float messageDuration = 3f;
    private Coroutine currentMessage;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void ShowMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message)) return;

        if (currentMessage != null)
            StopCoroutine(currentMessage);

        currentMessage = StartCoroutine(ShowMessageCoroutine(message));
    }

    private IEnumerator ShowMessageCoroutine(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(messageDuration);
        messageText.gameObject.SetActive(false);
    }
}
