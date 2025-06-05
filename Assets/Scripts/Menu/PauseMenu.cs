using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public Behaviour script;
    public ThirdPersonCamera cameraScript;
    public GameObject missionUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cameraScript != null)
        {
            cameraScript.enabled = true;
        }

        if (missionUI != null)
        {
            missionUI.SetActive(true);
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (cameraScript != null)
        {
            cameraScript.enabled = false;
        }

        if (missionUI != null)
        {
            missionUI.SetActive(false);
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Interface");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
