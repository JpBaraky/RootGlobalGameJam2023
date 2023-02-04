using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class catScript : MonoBehaviour
{
    public LayerMask whatIsGround;
    public int vidasGato ;
    public int vidaAtual;
    public float jumpForce;
    private Rigidbody2D corpoGato;
    private Animator animatorGato;
    private Transform transformGato;
    public bool lookRight;
    private float  horizontal;
    public float catSpeed;
    private Vector3 direcaoPeronagem = Vector3.right;
    public bool grounded;
    public Transform groundCheck;
    private bool jump;
    private bool isOnPlatform;
    public Rigidbody2D PlatformRB;
    public int timesFoundMom;
    public TextMeshProUGUI momCounter;



    // Start is called before the first frame update
    void Start()
    {
        corpoGato = GetComponent<Rigidbody2D>();
        animatorGato = GetComponent<Animator>();
        transformGato = GetComponent<Transform>();
        vidasGato = 7;

        vidaAtual = vidasGato;
    }

    private void FixedUpdate() {
        if(isOnPlatform) {
            corpoGato.velocity = new Vector2(PlatformRB.velocity.x,PlatformRB.velocity.y ) + corpoGato.velocity;
        }

        Jump();
    }
    void Update()
    {
        

        if(Input.GetButtonDown("Jump") && grounded) {
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
            Debug.Log("You Won!!!");
            timesFoundMom++;
            momCounter.text = timesFoundMom.ToString("N0");

            break;
            case "MovingPlatform":
                isOnPlatform= true;
                PlatformRB = collision.GetComponentInParent<Rigidbody2D>();
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
           // corpoGato.AddForce(Vector2.up * jumpForce);
            jump= false;
        }
    }
    void CheckGround() {
        grounded = Physics2D.OverlapCircle(groundCheck.position,0.03f, whatIsGround); // circulo que checa se o personagem esta tocando o chão
      
    }

}
