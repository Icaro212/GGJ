using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls_Script : MonoBehaviour
{
    
    public Rigidbody2D rb;

    public float speed= 5f;


    void Start(){
        rb=GetComponent<Rigidbody2D>();
    }

    
    void Update(){
        float horizontal = Input.GetAxis("Horizontal");

        if(horizontal<0){
            rb.velocity = new Vector2( rb.velocity.x * horizontal  * speed, rb.velocity.y );
        }
        
            
        
        

    }


}
