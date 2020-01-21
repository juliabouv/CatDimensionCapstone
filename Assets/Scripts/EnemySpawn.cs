using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] public GameObject enemy;
    [SerializeField] public Transform enemyPos;
    [SerializeField] private float repeatRate = 4.0f;
    public float destroyAfter = 30f;
    public AudioClip enemySoundSFX;
    public float soundVol = 0.5f;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InvokeRepeating("EnemySpawner", 1f, repeatRate);
            Destroy(gameObject, destroyAfter);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void EnemySpawner()
    {
        Instantiate(enemy, enemyPos.position, Quaternion.identity);
        GameObject audioListener = GameObject.FindWithTag("AudioListener");
        AudioSource.PlayClipAtPoint(enemySoundSFX, audioListener.transform.position, soundVol);
    }

}
