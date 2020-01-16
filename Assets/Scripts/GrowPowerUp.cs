using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPowerUp : MonoBehaviour
{

    public GameObject pickupEffect;
    public float multiplier = 1.4f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Pickup(collision);
        }
    }

    void Pickup(Collider2D player)
    {
        Instantiate(pickupEffect, transform.position, Quaternion.identity);

        player.transform.localScale *= multiplier;

        Destroy(gameObject);
    }
}
