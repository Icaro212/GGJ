using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls_Script : MonoBehaviour
{
    
    public Rigidbody2D rb;

    public float speed;

    public Collider2D character;

    void Start(){
        rb=GetComponent<Rigidbody2D>();
        character=GetComponent<CapsuleCollider2D>();
        
    }

    
    void Update(){
        float horizontal = Input.GetAxis("Horizontal");
        if(horizontal != 0){
            rb.velocity = new Vector2 (horizontal * speed * Time.fixedDeltaTime, rb.velocity.y);
        }

        if(Input.GetButton("Jump") && IsGrounded()){
            rb.AddForce(new Vector2(rb.velocity.x, (float) 0.8) ,ForceMode2D.Impulse);
        }
        
    }

    // bool IsGrounded(){
    //     Physics2D.Raycast(character.bounds.center,Vector2.down, character.bounds.extents.y+0.1f);
    //     Debug.DrawLine(character.bounds.center,Vector2.down,Color.blue,(float) 10000000);
    // }


}
