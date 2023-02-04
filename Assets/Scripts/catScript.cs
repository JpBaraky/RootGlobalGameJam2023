using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
