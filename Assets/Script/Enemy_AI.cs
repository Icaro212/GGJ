using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public float speed = 1;
    public Rigidbody2D rb;
    //Color state hace referencia al estado del "color" siendo: 0 Oscuro/ 1 A color
    public int colorstate = 0;
    public float dir = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (colorstate == 1)
        {
            rb.velocity = new Vector2(dir * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            
        }
        

    }
    

    private void OnTriggerEnter2D(Collider2D other) 
    {
        dir = -(dir);

        if (other.gameObject.tag == "Player" && colorstate == 1)
        {
            //Cuando el enemigo toca al jugador este se muere, WIP
            colorstate = 0;
        };
    }
}
