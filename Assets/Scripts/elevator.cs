using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : MonoBehaviour
{
    public Transform elevatorStartPosition;
    public Transform elevatorEndPosition;
    public GameObject elevatorObject;
    public float elevatorSpeed;
    public bool isHorizontal;
    public bool hitTrigger;
    private bool isMovingUp;
    public Rigidbody2D elevatorRB;
    public float elevatorWaitTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
    
        if(!isHorizontal) {
            if(isMovingUp && !hitTrigger) {
                elevatorRB.velocity = Vector2.up* elevatorSpeed;
      

            }
            if( !isMovingUp && !hitTrigger ) {
                elevatorRB.velocity = Vector2.down* elevatorSpeed;
               
            }
        }
        if(isHorizontal) {
            if(isMovingUp && !hitTrigger) {
                elevatorRB.velocity = Vector2.right * elevatorSpeed;
            

            }
            if(!isMovingUp && !hitTrigger) {
                elevatorRB.velocity = Vector2.left * elevatorSpeed;
            
            }
        }
    }
    void ElevatorTurn() {

        isMovingUp = !isMovingUp;
        hitTrigger = false;
    }

   void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "ElevatorTrigger") {
        hitTrigger = true;
        elevatorRB.velocity = Vector2.zero;
            StartCoroutine(waitElevator());
        }
        
    }
    public IEnumerator waitElevator() {
        yield return new WaitForSeconds(elevatorWaitTime);
        ElevatorTurn();
    }
}
