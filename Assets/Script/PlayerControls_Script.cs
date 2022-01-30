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

    public float colorTime = 5f;

    GameObject[] noColorList;

    GameObject[] colorList;

    GameObject[] fairyList;

    void Start(){
        rb=GetComponent<Rigidbody2D>();
        character=GetComponent<CapsuleCollider2D>();


        noColorList = GameManager.instance.noColorList;
        colorList = GameManager.instance.colorList;
        fairyList= GameManager.instance.fairyList;
    }

    
    void Update(){
        if(Input.GetButton("Cancel")){
            GameManager.instance.pause();
            Debug.Log("Time will continued now!");
            // GameManager.instance.resume();
        }

    }

    void FixedUpdate(){
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2 (horizontal * speed, rb.velocity.y);
        if(horizontal != 0){
            rb.velocity = movement;
        }
        if(Input.GetButton("Jump") && IsGrounded() ){
            rb.AddForce(new Vector2(movement.x, (movement.y + jumpforce)), ForceMode2D.Impulse);
        }
    }

    List<string> Lista_Escenarios = new List<string>()
        {
            //Renombrar a los GameStates actuales, de manera apilada
            "Level2",
            "Level1",
        };

    public void WinCond()
    {
        Lista_Escenarios.RemoveAt(Lista_Escenarios.Count -1);
        string victoria=Lista_Escenarios[Lista_Escenarios.Count-1];
        if (victoria != null)
        {
            GameManager.instance.ChangeScene(victoria);
        }
        else
        {
            GameManager.instance.ChangeScene("Main menu");
        }
    }


    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Fairy")){
            StartCoroutine(takeFairy());
        }
        if (other.gameObject.name.Equals("DoorWin"))
        {
            WinCond();
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.name.Equals("LavaFloor")){
            Destroy(gameObject);
            GameManager.instance.ChangeScene("");
        }
    }

    public IEnumerator takeFairy(){
        changeColor(false);
        playTransistion();
        yield return new WaitForSeconds(1f);
        changeAudio();
        yield return new WaitForSeconds(colorTime);
        changeColor(true);
        changeAudio();
    }

    public void changeColor(bool rBool){

        foreach (var i in noColorList)
        {
            i.SetActive(rBool);
        }

        foreach (var b in colorList)
        {
            b.SetActive(!rBool) ;
        }


        foreach(var j in fairyList){
            j.SetActive(rBool);
        }

        GameManager.instance.stateColor=!rBool;
    }
    public void playTransistion(){
        GameObject Camera =GameObject.FindGameObjectsWithTag("MainCamera")[0];//Size 1 only one camera
        AudioSource audio=Camera.GetComponent<AudioSource>();
        audio.clip=GameManager.instance.changeToColor;
        audio.loop=false;
        audio.Play();
        
    }

    public void fadeOut(){
        GameObject Camera =GameObject.FindGameObjectsWithTag("MainCamera")[0];//Size 1 only one camera
        AudioSource audio=Camera.GetComponent<AudioSource>();
        float volumenIncial=audio.volume;
        for(int i=0;i<5;i++){
            audio.volume=audio.volume - (float) 0.2;
        }
    }


    public void changeAudio(){
        bool color = GameManager.instance.stateColor;
        GameObject Camera =GameObject.FindGameObjectsWithTag("MainCamera")[0];//Size 1 only one camera
        AudioSource audio=Camera.GetComponent<AudioSource>();
        if(color){           
            audio.clip=GameManager.instance.colorMusic;
            audio.Play();
            audio.loop=true;

        }else{
 
            audio.clip=GameManager.instance.noColorMusic;
            audio.volume=(float) 1.0;
            audio.Play();
            audio.loop=true;
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


    void OnDestroy()
    {
        if (GameManager.instance.stateColor)
        {
            GameManager.instance.stateColor = false;
        }
    }

}
