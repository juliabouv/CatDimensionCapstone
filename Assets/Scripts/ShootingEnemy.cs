﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    private float timeBetweenShots;
    public float startTimeBetweenShots = 3f;
    public GameObject projectile;
    Animator myAnimator;

    public Transform player;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
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
        
    }

    public void killEnemy(GameObject enemy)
    {
        Destroy(enemy);
    }
}