using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatPickup : MonoBehaviour
{
    [SerializeField] AudioClip catPurrSFX;
    [SerializeField] float soundVol = 0.5f;

    bool treatPicked = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!treatPicked)
        {
            treatPicked = true;
            FindObjectOfType<Player>().AddLife();
            Destroy(gameObject);

            GameObject audioListener = GameObject.FindWithTag("AudioListener");
            AudioSource.PlayClipAtPoint(catPurrSFX, audioListener.transform.position, soundVol);
        }
    }
}
