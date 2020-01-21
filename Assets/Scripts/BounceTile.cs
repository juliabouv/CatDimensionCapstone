using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTile : MonoBehaviour
{

    [SerializeField] AudioClip bounceSFX;
    [SerializeField] float soundVol = 0.25f;

    Animator bounceAnimator;
    PolygonCollider2D triggerAnimation;

    void Start()
    {
        bounceAnimator = GetComponent<Animator>();
        triggerAnimation = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        bool playerIsBouncing = triggerAnimation.IsTouchingLayers(LayerMask.GetMask("Player"));

        bounceAnimator.SetBool("Bouncing", (playerIsBouncing));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool playerIsBouncing = triggerAnimation.IsTouchingLayers(LayerMask.GetMask("Player"));

        Debug.Log(playerIsBouncing);
        bounceAnimator.SetBool("Bouncing", (playerIsBouncing));
    }

    public void BounceSound()
    {
        GameObject audioListener = GameObject.FindWithTag("AudioListener");
        AudioSource.PlayClipAtPoint(bounceSFX, audioListener.transform.position, soundVol);
    }
}



