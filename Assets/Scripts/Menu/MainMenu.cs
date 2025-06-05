using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Cena")]
    [SerializeField] private string gameNameLevel = "Scenes/InGame";

    [Header("Menus")]
    [SerializeField] private GameObject MenuInicial;
    [SerializeField] private GameObject Opcoes;
    [SerializeField] private GameObject Creditos;

    [Header("Áudio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip playButtonSound;

    [Header("Referência da Câmera")]
    [SerializeField] private CameraMenu cameraMenu;

    [Header("Tela de Introdução")]
    [SerializeField] private GameObject introPanel;
    [SerializeField] private CanvasGroup introCanvasGroup;
    [SerializeField] private float introDuration = 5f;
    [SerializeField] private float fadeDuration = 1f;

    public static bool MenuCreditos = false;

    public void Play()
    {
        if (audioSource != null && playButtonSound != null)
        {
            audioSource.PlayOneShot(playButtonSound);
        }

        StartCoroutine(ShowIntroAndStartGame());
    }

    private IEnumerator ShowIntroAndStartGame()
    {
        introPanel.SetActive(true);

        yield return StartCoroutine(FadeCanvasGroup(introCanvasGroup, 0, 1, fadeDuration));
        yield return new WaitForSeconds(introDuration);
        yield return StartCoroutine(FadeCanvasGroup(introCanvasGroup, 1, 0, fadeDuration));

        SceneManager.LoadScene("Scenes/InGame");
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;
        canvasGroup.alpha = startAlpha;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }

    public void Options()
    {
        MenuInicial.SetActive(false);
        Opcoes.SetActive(true);
        cameraMenu.MoveToOptions();
    }

    public void CloseOprions()
    {
        Opcoes.SetActive(false);
        MenuInicial.SetActive(true);
        cameraMenu.ResetCamera();
    }

    public void OpenCreditos()
    {
        MenuInicial.SetActive(false);
        Creditos.SetActive(true);
        MenuCreditos = true;
        cameraMenu.MoveToCredits();
    }

    public void CloseCreditos()
    {
        Creditos.SetActive(false);
        MenuInicial.SetActive(true);
        MenuCreditos = false;
        cameraMenu.ResetCamera();
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
