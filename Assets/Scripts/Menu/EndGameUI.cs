using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    public GameObject endScreen;

    public void ShowEndScreen()
    {
        endScreen.SetActive(true);
        Time.timeScale = 0f; // pausa o jogo
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
