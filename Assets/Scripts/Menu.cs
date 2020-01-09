using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void StartFirstLevel()
    {
        SceneManager.LoadScene(1);
        //int playerLives = FindObjectOfType<GameSession>().playerLives = 9;
        //int score = FindObjectOfType<GameSession>().score = 0;
        //FindObjectOfType<GameSession>().livesText.text = playerLives.ToString();
        //FindObjectOfType<GameSession>().scoreText.text = score.ToString();
    }

    public void LoadMainMenu()
    {
        FindObjectOfType<GameSession>().ResetGameSession();
        
        SceneManager.LoadScene(0);
    }
}