using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPowerUp : MonoBehaviour
{

    public GameObject pickupEffect;
    public float multiplier = 1.4f;
    public float duration = 8f;
    [SerializeField] AudioClip powerupPickupSFX;
    [SerializeField] float soundVol = 0.4f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine( Pickup(collision) );
        }
    }

    IEnumerator Pickup(Collider2D player)
    {
        FindObjectOfType<Player>().vulnerability = false;

        Instantiate(pickupEffect, transform.position, transform.rotation);

        GameObject audioListener = GameObject.FindWithTag("AudioListener");
        AudioSource audioSource = audioListener.GetComponent<AudioSource>();
        audioSource.Pause();
        AudioSource.PlayClipAtPoint(powerupPickupSFX, audioListener.transform.position, soundVol);

        player.transform.localScale = new Vector3 (multiplier, multiplier, 0);

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(duration);

        audioSource.Play();
        FindObjectOfType<Player>().vulnerability = true;
        player.transform.localScale = new Vector3(1, 1, 0);

        Destroy(gameObject);
    }
}