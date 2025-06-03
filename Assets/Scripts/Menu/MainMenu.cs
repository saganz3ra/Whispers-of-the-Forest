using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string gameNameLevel;

    [SerializeField]
    private GameObject MenuInicial;

    [SerializeField]
    private GameObject Opcoes;

    [SerializeField]
    private GameObject Creditos;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip playButtonSound;
    public static bool MenuCreditos = false;

    [SerializeField]
    private CameraMenu cameraMenu; // Referência ao script CameraMenu

    public void Play()
    {
        if (audioSource != null && playButtonSound != null)
        {
            audioSource.PlayOneShot(playButtonSound);
        }
        SceneManager.LoadScene("Game");
    }

    public void Options()
    {
        MenuInicial.SetActive(false);
        Opcoes.SetActive(true);
        cameraMenu.MoveToOptions(); // Move a câmera para Opções
    }

    public void CloseOprions()
    {
        Opcoes.SetActive(false);
        MenuInicial.SetActive(true);
        cameraMenu.ResetCamera(); // Volta a câmera para o menu inicial
    }

    public void OpenCreditos()
    {
        MenuInicial.SetActive(false);
        Creditos.SetActive(true);
        MenuCreditos = true;
        cameraMenu.MoveToCredits(); // Move a câmera para Créditos
    }

    public void CloseCreditos()
    {
        Creditos.SetActive(false);
        MenuInicial.SetActive(true);
        MenuCreditos = false;
        cameraMenu.ResetCamera(); // Volta a câmera para o menu inicial
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}