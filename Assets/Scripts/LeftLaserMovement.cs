using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftLaserMovement : MonoBehaviour
{

    [SerializeField] float horizontalSpeed = 5f;

    Rigidbody2D myRigidBody;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         myRigidBody.velocity = new Vector2(-horizontalSpeed, 0f);
    }
}