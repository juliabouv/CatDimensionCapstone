using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour
{

    [SerializeField] float horizontalSpeed = 3f;
    [SerializeField] float verticalSpeed = 2f;
    Rigidbody2D myRigidBody;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(horizontalSpeed, verticalSpeed);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-horizontalSpeed, -verticalSpeed);
        }
    }

    public void killEnemy(GameObject enemy)
    {
        Destroy(enemy);
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), -(Mathf.Sign(myRigidBody.velocity.y)));
    //}
}