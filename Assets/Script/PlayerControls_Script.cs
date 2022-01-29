using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls_Script : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public float speed;
    public Collider2D character;
    public LayerMask layerMask;
    public float jumpforce;

    public bool colorCheck = false;

    public float colorTime = 2f;

    GameObject[] noColorList;

    GameObject[] colorList;

    void Start(){
        rb=GetComponent<Rigidbody2D>();
        character=GetComponent<CapsuleCollider2D>();


        noColorList = GameObject.FindGameObjectsWithTag("noColor");
        colorList = GameObject.FindGameObjectsWithTag("color");
    }

    
    void Update(){
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2 (horizontal * speed * Time.fixedDeltaTime, rb.velocity.y);
        if(horizontal != 0){
            rb.velocity = movement;
        }
        
        if(Input.GetButton("Jump") && IsGrounded() ){
            rb.AddForce(new Vector2(movement.x, (movement.y + jumpforce) * Time.fixedDeltaTime), ForceMode2D.Impulse);
        }

    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            StartCoroutine(takeFairy());
        }
    }

    public IEnumerator takeFairy()
    {
        changeColor(false);
        yield return new WaitForSeconds(colorTime);
        changeColor(true);
    }

    public void changeColor(bool rBool)
    {
        
        foreach (var i in noColorList)
        {
            i.GetComponent<SpriteRenderer>().enabled = rBool;
        }
       
        foreach (var b in colorList)
        {
            b.GetComponent<SpriteRenderer>().enabled = !rBool; ;
        }
    }
    bool IsGrounded(){
        RaycastHit2D rayCastHit=Physics2D.Raycast(character.bounds.center,Vector2.down, character.bounds.extents.y+0.1f, layerMask);
        if(rayCastHit.collider == null){
            return false;
        }else{
            return true;
        };

    }


}
