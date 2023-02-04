using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class catMeow : MonoBehaviour
{
    public AudioSource catSound;
    public AudioSource momSound;
    public ParticleSystem rippleEffect;
    public ParticleSystem rippleEffectMom;
    public int numberOfMeows;
    public TextMeshProUGUI meowCounter;
    private catScript catScript;
    private GameController gameController;
    private int lastMeows;

    void Start()
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        catScript = FindObjectOfType(typeof(catScript)) as catScript;
    }


    void Update()
    {
        if(gameController.currentState != GameController.GameState.GAMEPLAY) {
            return;
        }
        
        if(Input.GetButtonDown("Meow") && !catSound.isPlaying) {
            StartCoroutine(CatMeow());
            numberOfMeows++;
            if(catScript.timesFoundMom >= 5) {
                lastMeows++;
                if(lastMeows == 3) {
                    gameController.currentState = GameController.GameState.GAMESTOP;
                }
            }
        }
        if(meowCounter != null) {
            meowCounter.text = numberOfMeows.ToString("N0");

        }
    }
    IEnumerator CatMeow() {
        catSound.Play();
        rippleEffect.Play();
        yield return new WaitWhile(() => catSound.isPlaying);
        yield return new WaitForSeconds(1);
        if(catScript.timesFoundMom < 5) {

            rippleEffectMom.Play();
            momSound.Play();
        }
    }
}
