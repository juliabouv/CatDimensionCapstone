using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBoss : MonoBehaviour
{
    public float moveSpeed = 5f;
    Rigidbody2D myRigidBody;
    public Transform player;
    private Vector2 movement;
    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        myRigidBody.rotation = angle;

        direction.Normalize();
        movement = direction;

        //myRigidBody.velocity = new Vector2(moveSpeed, 0f);

    }

    private void FixedUpdate()
    {
        moveEnemy(movement);
    }

    void moveEnemy(Vector2 direction)
    {
        myRigidBody.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}
