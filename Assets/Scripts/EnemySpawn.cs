﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] public GameObject enemy;
    [SerializeField] public Transform enemyPos;
    [SerializeField] private float repeatRate = 2.0f;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InvokeRepeating("EnemySpawner", 0.5f, repeatRate);
            Destroy(gameObject, 20);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void EnemySpawner()
    {
        Instantiate(enemy, enemyPos.position, Quaternion.identity);
        Debug.Log("MEthod Called");
    }

}
