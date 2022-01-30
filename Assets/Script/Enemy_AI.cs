using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public float speed = 1;
    public Rigidbody2D rb;
    public float dir = 1;
    public float frameNumber;
    public float Timelimit = 1000;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (GameManager.instance.stateColor == true)
        {
            if (frameNumber >= Timelimit)
            {
                dir = -(dir);
                frameNumber = 0;
            }
            else
            {
                frameNumber = frameNumber + 1;
            };
            rb.velocity = new Vector2(dir * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);

            
        }
        

    }
    

    private void OnTriggerEnter2D(Collider2D other) 
    {

        if (other.gameObject.tag == "Player" && GameManager.instance.stateColor == true)
        {
            GameManager.instance.ChangeScene("");
            
        };
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.name.Equals("LavaFloor")){
            Destroy(gameObject);
        }
    }

    
}
