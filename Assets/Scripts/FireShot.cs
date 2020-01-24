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
        Debug.Log("Colliding with" + collision.gameObject.name);
        if (shot.IsTouchingLayers(LayerMask.GetMask("Enemy")) && collision.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<EnemyMovement>().killEnemy(collision.gameObject);
        }

        if (shot.IsTouchingLayers(LayerMask.GetMask("CatLady")))
        {
            FindObjectOfType<CatLadyBoss>().TakeDamage(5);
        }

        Destroy(gameObject);
    }
}
