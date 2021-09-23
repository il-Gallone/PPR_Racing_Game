using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;

    public GameObject pauseMenuUI;

    public void Resume()
    {
        gamePaused = false;
        pauseMenuUI.SetActive(false);

        Time.timeScale = 1f;

        Cursor.visible = false;
    }

    void Pause()
    {
        gamePaused = true;
        pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;

        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        gamePaused = false;

        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
                Resume();
            else
                Pause();
        }
    }
}
