using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour {

    [SerializeField] public int playerLives = 9;
    [SerializeField] public int score = 0;
    [SerializeField] public int maxHealth = 4;
    [SerializeField] public int currentHealth = 2;

    [SerializeField] float DeathLoadDelay = 1f;

    [SerializeField] public TextMeshProUGUI livesText;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public Image healthBar;

    int addedLives = 0;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            Debug.Log("Game Session destroyed");
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
	}

    public void AddToScore (int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
            FindObjectOfType<CatLadyBoss>().ResetHealth();
            FindObjectOfType<BoxBoss>().ResetHealth();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        currentHealth = 2;
        UpdateHealthBar();

        StartCoroutine(SlowDeathLoad());
    }

    public void IncreaseLife()
    {
        int newLives = (score - addedLives * 1000) / 1000;
        if (newLives == 1)
        {
            playerLives += newLives;
            addedLives++;
            livesText.text = playerLives.ToString();
        }
    }

    IEnumerator SlowDeathLoad()
    {
        yield return new WaitForSecondsRealtime(DeathLoadDelay);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    public void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}