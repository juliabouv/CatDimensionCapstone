using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    private float timeBetweenShots;
    public float startTimeBetweenShots = 3f;
    public GameObject projectile;
    Animator myAnimator;
    Rigidbody2D myRigidBody;
    public AudioClip enemyDeathSFX;
    public float soundVol = 0.25f;

    public Transform player;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();

        timeBetweenShots = startTimeBetweenShots;
    }

    
    void Update()
    {
        if (timeBetweenShots <= 0)
        {
            myAnimator.SetBool("Throw", true);
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            myAnimator.SetBool("Throw", false);
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

    public void killEnemy(GameObject enemy)
    {
        GameObject audioListener = GameObject.FindWithTag("AudioListener");
        AudioSource.PlayClipAtPoint(enemyDeathSFX, audioListener.transform.position, soundVol);
        Destroy(enemy);
    }

    void flipSprite()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }

    bool IsFacingRight()
    {
        return transform.localScale.x < 0;
    }
}
