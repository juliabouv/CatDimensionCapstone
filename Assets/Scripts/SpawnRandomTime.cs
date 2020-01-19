using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomTime : MonoBehaviour
{

    public GameObject toSpawn;
    public bool stopSpawning = false;
    public float spawnTime = 1f;
    public float spawnDelay = 15f;


    private void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
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
