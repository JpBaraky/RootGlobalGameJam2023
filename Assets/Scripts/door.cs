using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    private fadeBackground fadeBackground;

    public catScript catScript;
    public Transform destination;
    private GameController gameController;
    private randomPosition randomPosition;
    // Start is called before the first frame update
    void Start()
    {   randomPosition = FindObjectOfType(typeof (randomPosition)) as randomPosition;
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        fadeBackground = FindObjectOfType(typeof(fadeBackground)) as fadeBackground;
        catScript = FindObjectOfType(typeof(catScript)) as catScript;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Teleport() {
        StartCoroutine(triggerDoor());
    }
    IEnumerator triggerDoor() {
        fadeBackground.fadeIn();
        yield return new WaitWhile(() => fadeBackground.fume.color.a < 0.9f); 
        catScript.transform.position = destination.position;
        randomPosition.RandomPosition();
        fadeBackground.fadeOut();
        gameController.currentState = GameController.GameState.GAMEPLAY;

    }
}
