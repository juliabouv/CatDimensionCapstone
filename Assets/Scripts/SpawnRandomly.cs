using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomly : MonoBehaviour
{
    public GameObject toSpawn;
    public bool stopSpawning = false;
    public float spawnTime = 0.0001f;
    public float spawnDelay = 3f;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
            Destroy(gameObject, 15);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void SpawnObject()
    {
        Instantiate(toSpawn, transform.position, Quaternion.identity);
        if(stopSpawning)
        {
            CancelInvoke("SpawnObject");
        }
    }
}
