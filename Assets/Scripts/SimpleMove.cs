using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;
    void Update()
    {
        transform.Translate(-Vector2.right * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Circle (1)")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
