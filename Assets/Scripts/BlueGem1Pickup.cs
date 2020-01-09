using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGem1Pickup : MonoBehaviour
{
    [SerializeField] AudioClip gemPickUpSFX;
    [SerializeField] float soundVol = 0.25f;
    [SerializeField] int pointsForGemPickup = 200;
    bool gemPicked = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gemPicked)
        {
            gemPicked = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForGemPickup);
            Destroy(gameObject);

            GameObject audioListener = GameObject.FindWithTag("AudioListener");
            AudioSource.PlayClipAtPoint(gemPickUpSFX, audioListener.transform.position, soundVol);
        }
    }
}
