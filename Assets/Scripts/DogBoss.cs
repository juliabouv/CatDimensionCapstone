using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBoss : MonoBehaviour
{
    public float moveSpeed = 5f;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    public Transform player;
    private Vector2 movement;
    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        myRigidBody.rotation = angle;

        direction.Normalize();
        movement = direction;
        
    }

    private void FixedUpdate()
    {

        moveEnemy(movement);
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (myRigidBody.IsTouchingLayers(LayerMask.GetMask("Dog Bait")))
        {
            Vector2 dogVelocity = new Vector2(0f, 0f);
            myRigidBody.velocity = dogVelocity;
            myAnimator.SetTrigger("Dog Peeing");
        }
    }

    void moveEnemy(Vector2 direction)
    {
        myRigidBody.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
