using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catScript : MonoBehaviour
{
    public int vidasGato ;
    public int vidaAtual;
    private Rigidbody2D corpoGato;
    private Animator animatorGato;
    private Transform transformGato;
    public bool lookRight;
    private float  horizontal;
    public float catSpeed;
    private Vector3 direcaoPeronagem = Vector3.right;



    // Start is called before the first frame update
    void Start()
    {
        corpoGato = GetComponent<Rigidbody2D>();
        animatorGato = GetComponent<Animator>();
        transformGato = GetComponent<Transform>();
        vidasGato = 7;

        vidaAtual = vidasGato;
    }

    // Update is called once per frame
    void Update()
    {
        if(vidaAtual == 0 ){
        
        
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
}
