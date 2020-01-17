using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShot : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;
    BoxCollider2D shot;

    void Start()
    {
        rb.velocity = transform.right * speed;
        shot = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colliding with" + collision);
        if (shot.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            FindObjectOfType<EnemyMovement>().killEnemy(collision.gameObject);
            Debug.Log("We are also here");
        }

        Destroy(gameObject);
    }
}
