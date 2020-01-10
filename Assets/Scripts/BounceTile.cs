using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTile : MonoBehaviour
{

    [SerializeField] AudioClip bounceSFX;
    [SerializeField] float soundVol = 0.25f;

    Animator bounceAnimator;

    void Start()
    {
        bounceAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bounceAnimator.SetTrigger("Tile Bounce");

        GameObject audioListener = GameObject.FindWithTag("AudioListener");
        AudioSource.PlayClipAtPoint(bounceSFX, audioListener.transform.position, soundVol);

    }
}