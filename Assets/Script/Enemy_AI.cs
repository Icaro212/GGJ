using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public float speed = 1;
    public float minX;
    public float maxX;
    public Rigidbody2D rb;
    public float dir = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        rb.velocity = new Vector2( dir * speed, rb.velocity.y);

    }
    

    private void OnTriggerEnter2D(Collider2D other) 
    {
        dir = -(dir);
    }
}
