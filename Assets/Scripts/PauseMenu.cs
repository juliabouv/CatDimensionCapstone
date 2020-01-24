using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    
    void Update()
    {
        if (gameIsPaused)
        {
            pauseMenuUI.SetActive(true);
        }
        else
        {
            pauseMenuUI.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
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
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        FindObjectOfType<GameSession>().ResetGameSession();
    }

    public void RestartLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
