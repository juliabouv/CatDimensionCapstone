﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CatLadyBoss : MonoBehaviour
{

    public int health = 100;
    public float moveSpeed = 2f;
    public float stoppingDistance;
    public float retreatDistance;
    public float loadDelay = 2f;
    public Slider healthBar;
    public AudioClip enemyDeathSFX;
    public AudioClip deathSFX;
    public float soundVol = 0.25f;

    private float timeBetweenShots;
    public float startTimeBetweenShots = 2f;
    public GameObject projectile;

    public Transform player;

    Animator myAnimator;
    Rigidbody2D myRigidBody;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();

        timeBetweenShots = startTimeBetweenShots;
    }

    
    void Update()
    {
        healthBar.value = health;

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            myAnimator.SetTrigger("Walking");
        }
        else if((Vector2.Distance(transform.position, player.position) < stoppingDistance) && (Vector2.Distance(transform.position, player.position) > retreatDistance))
        {
            transform.position = this.transform.position;
            myAnimator.SetTrigger("Idle");
        }
        else if(Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
            myAnimator.SetTrigger("Walking");
        }

        if(timeBetweenShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }


        if (IsFacingRight() && player.position.x < transform.position.x)
        {
            flipSprite();
        }
        else if ((!IsFacingRight()) && player.position.x > transform.position.x)
        {
            flipSprite();
        }
    }

    void flipSprite()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    public void TakeDamage(int damage)
    {
        GameObject audioListener = GameObject.FindWithTag("AudioListener");
        AudioSource.PlayClipAtPoint(enemyDeathSFX, audioListener.transform.position, soundVol);
        health -= damage;

        if(health <= 0)
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
        FindObjectOfType<GameSession>().AddToScore(500);
        StartCoroutine(SlowLoad());
    }

    IEnumerator SlowLoad()
    {
        FindObjectOfType<Player>().vulnerability = false;
        startTimeBetweenShots = 100f;
        myAnimator.SetTrigger("Death");
        yield return new WaitForSecondsRealtime(loadDelay);

        FindObjectOfType<Player>().vulnerability = true;
        health = 100;
        startTimeBetweenShots = 2f;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
