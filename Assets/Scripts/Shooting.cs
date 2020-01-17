using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject fireProjectile;
    public float speed = 20f;

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            GameObject instFireProjectile = Instantiate(fireProjectile, transform.position, Quaternion.identity) as GameObject;
            Rigidbody2D instFireProjectileRigidbody = instFireProjectile.GetComponent<Rigidbody2D>();
        }
    }
}
