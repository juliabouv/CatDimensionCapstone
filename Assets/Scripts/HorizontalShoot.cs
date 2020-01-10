using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalShoot : MonoBehaviour
{
    [Tooltip("Game units per second")]
    [SerializeField] float scrollRate = 2f;

    void Update()
    {
        float xMove = scrollRate * Time.deltaTime;
        transform.Translate(new Vector2(xMove, 0f));
    }
}
