using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;

    private Transform player;
    private Vector2 target;
    private CircleCollider2D deathCircle;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        deathCircle = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(deathCircle.IsTouching(FindObjectOfType<Player>().myBodyCollider))
        {
            FindObjectOfType<Player>().PlayerInjured();
            Debug.Log("Injure Player");
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
