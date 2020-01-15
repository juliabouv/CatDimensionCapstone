using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTile : MonoBehaviour
{

    [SerializeField] AudioClip bounceSFX;
    [SerializeField] float soundVol = 0.25f;

    Animator bounceAnimator;
    BoxCollider2D bounceArea;

    void Start()
    {
        bounceAnimator = GetComponent<Animator>();
        bounceArea = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        BounceAnimation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bounceArea.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            GameObject audioListener = GameObject.FindWithTag("AudioListener");
            AudioSource.PlayClipAtPoint(bounceSFX, audioListener.transform.position, soundVol);
        }
    }

    private void BounceAnimation()
    {
        bool playerIsBouncing = bounceArea.IsTouchingLayers(LayerMask.GetMask("Player"));
        bounceAnimator.SetBool("Bouncing", playerIsBouncing);
    }
}