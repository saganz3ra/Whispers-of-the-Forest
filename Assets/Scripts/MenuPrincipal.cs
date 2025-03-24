using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private string gameNameLevel;
    [SerializeField] private GameObject MenuInicial;
    [SerializeField] private GameObject Opcoes;
    [SerializeField] private GameObject Creditos;
    [SerializeField] private AudioSource audioSource; // Referência ao AudioSource
    [SerializeField] private AudioClip playButtonSound; // Som do botão Play
    public static bool MenuCreditos = false;

    public void Play()
    {
        if (audioSource != null && playButtonSound != null)
        {
            audioSource.PlayOneShot(playButtonSound); // Toca o som
        }
        SceneManager.LoadScene("Interface");
    }

    public void Options()
    {
        MenuInicial.SetActive(false);
        Opcoes.SetActive(true);
    }

    public void CloseOprions()
    {
        Opcoes.SetActive(false);
        MenuInicial.SetActive(true);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void OpenCreditos()
    {
        MenuInicial.SetActive(false);
        Creditos.SetActive(true);
        MenuCreditos = true;
    }

    public void CloseCreditos()
    {
        Creditos.SetActive(false);
        MenuInicial.SetActive(true);
        MenuCreditos = false;
    }

    private void Update()
    {
        if (MenuCreditos && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCreditos();
        }
    }
}