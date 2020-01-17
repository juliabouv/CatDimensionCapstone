using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePowerUp : MonoBehaviour
{

    public GameObject pickupEffect;
    public float duration = 11f;
    [SerializeField] AudioClip powerupPickupSFX;
    [SerializeField] float soundVol = 0.4f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Pickup(collision));
        }
    }

    IEnumerator Pickup(Collider2D player)
    {
        FindObjectOfType<Player>().firePowerup = true;

        //Instantiate(pickupEffect, transform.position, tranform.rotation);

        GameObject audioListener = GameObject.FindWithTag("AudioListener");
        AudioSource audioSource = audioListener.GetComponent<AudioSource>();
        audioSource.Pause();
        AudioSource.PlayClipAtPoint(powerupPickupSFX, audioListener.transform.position, soundVol);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(duration);

        audioSource.Play();
        FindObjectOfType<Player>().firePowerup = false;

        Destroy(gameObject);
    }
}
