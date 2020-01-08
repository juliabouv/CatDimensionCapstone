using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

    [SerializeField] AudioClip coinPickUpSFX;
    [SerializeField] float soundVol = 0.25f;
    [SerializeField] int pointsForCoinPickup = 100;
	bool coinPicked = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (!coinPicked)
		{
            coinPicked = true;
			FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
			Destroy(gameObject);

            GameObject audioListener = GameObject.FindWithTag("AudioListener");
            AudioSource.PlayClipAtPoint(coinPickUpSFX, audioListener.transform.position, soundVol);
        }
    }
}