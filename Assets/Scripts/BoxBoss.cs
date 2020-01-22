using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoxBoss : MonoBehaviour
{
    public int health = 100;
    public float timer;
    public float loadDelay = 2f;
    public Slider healthBar;
    public AudioClip enemyDeathSFX;
    public AudioClip deathSFX;
    public float soundVol = 0.25f;

    Animator animator;
    public BoxCollider2D receiveDamageCollider;
    public CapsuleCollider2D causeDamageCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        receiveDamageCollider = GetComponent<BoxCollider2D>();
        causeDamageCollider = GetComponent<CapsuleCollider2D>();
    }


    void Update()
    {
        healthBar.value = health;
    }

    public void TakeDamage(int damage)
    {
        GameObject audioListener = GameObject.FindWithTag("AudioListener");
        AudioSource.PlayClipAtPoint(enemyDeathSFX, audioListener.transform.position, soundVol);
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        health = 100;
        healthBar.value = health;
    }

    void Die()
    {
        GameObject audioListener = GameObject.FindWithTag("AudioListener");
        AudioSource.PlayClipAtPoint(deathSFX, audioListener.transform.position, soundVol);
        FindObjectOfType<GameSession>().AddToScore(200);
        StartCoroutine(SlowLoad());
    }

    IEnumerator SlowLoad()
    {
        FindObjectOfType<Player>().vulnerability = false;
        animator.SetTrigger("Death");
        yield return new WaitForSecondsRealtime(loadDelay);
        health = 100;

        Destroy(FindObjectOfType<ScenePersist>());
        FindObjectOfType<Player>().vulnerability = true;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
