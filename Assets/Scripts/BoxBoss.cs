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
    public float soundVol = 0.25f;

    Animator animator;
    public BoxCollider2D receiveDamageCollider;
    public PolygonCollider2D causeDamageCollider;

    void Start()
    {
        animator = GetComponent<Animator>();
        receiveDamageCollider = GetComponent<BoxCollider2D>();
        causeDamageCollider = GetComponent<PolygonCollider2D>();
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
        FindObjectOfType<GameSession>().AddToScore(200);
        StartCoroutine(SlowLoad());
    }

    IEnumerator SlowLoad()
    {
        animator.SetTrigger("Death");
        yield return new WaitForSecondsRealtime(loadDelay);
        health = 100;

        Destroy(FindObjectOfType<ScenePersist>());
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
