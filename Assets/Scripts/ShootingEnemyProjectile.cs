using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyProjectile : MonoBehaviour
{
    public float speed = 5f;

    private Transform shootHere;
    private Vector2 target;
    private BoxCollider2D deathBox;

    private void Start()
    {
        shootHere = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(shootHere.position.x, shootHere.position.y);
        deathBox = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }

        if (deathBox.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (deathBox.IsTouching(FindObjectOfType<Player>().myBodyCollider))
        {
            if (FindObjectOfType<Player>().vulnerability)
            {
                FindObjectOfType<Player>().PlayerInjured();
                Debug.Log("Injure Player");
                DestroyProjectile();
            }
        }
        else
        {
            //DestroyProjectile();
        }
        Debug.Log(collision.gameObject.name);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
