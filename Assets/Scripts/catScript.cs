using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class catScript: MonoBehaviour {
    public LayerMask whatIsGround;
    public int vidasGato;
    public int vidaAtual;
    public float jumpForce;
    private Rigidbody2D corpoGato;
    private Animator animatorGato;
    private Transform transformGato;
    public bool lookRight;
    private float horizontal;
    public float catSpeed;
    private Vector3 direcaoPeronagem = Vector3.right;
    public bool grounded;
    public Transform groundCheck;
    private bool jump;
    private bool isOnPlatform;
    public Rigidbody2D PlatformRB;
    public int timesFoundMom;
    public TextMeshProUGUI momCounter;
    private GameController gameController;
    public GameObject exclamationMark;
   



    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        corpoGato = GetComponent<Rigidbody2D>();
        animatorGato = GetComponent<Animator>();
        transformGato = GetComponent<Transform>();
        vidasGato = 7;

        vidaAtual = vidasGato;
    }

    private void FixedUpdate() {
        if(gameController.currentState != GameController.GameState.GAMEPLAY) {
            return;
        }
        if(isOnPlatform) {
            corpoGato.velocity = new Vector2(PlatformRB.velocity.x,PlatformRB.velocity.y ) + corpoGato.velocity;
        }

        Jump();
    }
    void Update()
    {
        
        if(gameController.currentState != GameController.GameState.GAMEPLAY) {
            corpoGato.velocity = new Vector2(0,0);
            animatorGato.SetBool("GatoAndando",false);
            return;
        }
        if(lookRight) {
            this.transform.localScale = new Vector3(Mathf.Sign(this.transform.localScale.x) * 1 - (0.33f * timesFoundMom),Mathf.Sign(this.transform.localScale.y) * 1 + (0.33f * timesFoundMom),this.transform.localScale.z);
        } else {
            this.transform.localScale = new Vector3(Mathf.Sign(this.transform.localScale.x) * 1 + (0.33f * timesFoundMom),Mathf.Sign(this.transform.localScale.y) * 1 + (0.33f * timesFoundMom),this.transform.localScale.z);
        }
        if(Input.GetButtonDown("Jump") && grounded ) {
            jump = true;
 
        }
        AndarGato();
        if (horizontal > 0 && lookRight)
        {
            FlipCat();
        }
        else if (horizontal < 0 && !lookRight)
        {
            FlipCat();
        }
        CheckGround();
        //---
        exclamationMark.transform.localScale = new Vector3(Mathf.Sign(this.transform.localScale.x), Mathf.Sign(this.transform.localScale.y), 1);
    }
    void AndarGato(){
    horizontal = Input.GetAxis("Horizontal");
    corpoGato.velocity = new Vector2(horizontal * catSpeed, corpoGato.velocity.y);
        
        if(horizontal != 0){
    animatorGato.SetBool("GatoAndando", true);
    }
    else{
         animatorGato.SetBool("GatoAndando", false);
    }
    }
    void FlipCat(){
        lookRight = !lookRight;
       float x = transform.localScale.x;
            x *= -1; //inverter sinal int ou float;
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z); //passa valor do X novo para o scale.
            
            direcaoPeronagem.x = x;

    }
    void OnTriggerEnter2D(Collider2D collision) {
        switch(collision.gameObject.tag) {

            default: break;
            case "Win":
            collision.gameObject.SendMessage("Teleport",SendMessageOptions.DontRequireReceiver);
            
            gameController.currentState = GameController.GameState.GAMESTOP;
            Debug.Log("You Won!!!");
            corpoGato.velocity = new Vector2(0,0);
            animatorGato.SetBool("GatoAndando",false);
            timesFoundMom++;
            
            momCounter.text = timesFoundMom.ToString("N0");

            break;
            case "MovingPlatform":
                isOnPlatform= true;
                PlatformRB = collision.GetComponentInParent<Rigidbody2D>();
            break;
            case "Teleport":
            collision.gameObject.SendMessage("Teleport",SendMessageOptions.DontRequireReceiver);
            gameController.currentState = GameController.GameState.GAMESTOP;
            break;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision) {
        switch(collision.gameObject.tag) {

            default:
            break;
          
            case "MovingPlatform":
            isOnPlatform= false;

            break;

        }
    }
    void Jump() {
        if(jump) {
          corpoGato.velocity = Vector2.up * jumpForce;
           
            jump= false;
        }
    }
    void CheckGround() {
        grounded = Physics2D.OverlapCircle(groundCheck.position,0.03f, whatIsGround); // circulo que checa se o personagem esta tocando o chão
      
    }
    

}
