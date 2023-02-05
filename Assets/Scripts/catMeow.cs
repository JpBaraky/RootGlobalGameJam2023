using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class catMeow : MonoBehaviour
{
    public AudioSource catSound;
    public AudioSource momSound;
    public AudioSource newMeow;
    public ParticleSystem rippleEffect;
    public ParticleSystem rippleEffectMom;
    public int numberOfMeows;
    public TextMeshProUGUI meowCounter;
    private catScript catScript;
    private GameController gameController;
    private changeScene changeScene;
    private int lastMeows;
    bool isMeowing;
    public GameObject exclamationMark;

    void Start()
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        changeScene = FindObjectOfType(typeof(changeScene)) as changeScene;
        catScript = FindObjectOfType(typeof(catScript)) as catScript;
    }


    void Update()
    {
        if(gameController.currentState != GameController.GameState.GAMEPLAY) {
            return;
        }
        
        if(Input.GetButtonDown("Meow") && !catSound.isPlaying && !isMeowing) {
            StartCoroutine(CatMeow());
            numberOfMeows++;
            if(catScript.timesFoundMom >= 3) {
                lastMeows++;
                if(lastMeows == 3) {
                   
                  
                    StartCoroutine(EndMeow());
                }
            }
        }
        if(meowCounter != null) {
            meowCounter.text = numberOfMeows.ToString("N0");

        }
    }
    IEnumerator CatMeow() {
        isMeowing = true;
        catSound.pitch = Random.Range(1,1.2f);
        catSound.Play();
        rippleEffect.Play();
        yield return new WaitWhile(() => catSound.isPlaying);
        
        if(catScript.timesFoundMom < 3) {
            momSound.pitch = Random.Range(0.7f,0.9f);
            yield return new WaitForSeconds(1);
            rippleEffectMom.Play();
            momSound.Play();
        }
        isMeowing= false;
    }
    IEnumerator EndMeow() {

        gameController.currentState = GameController.GameState.GAMESTOP;
        yield return new WaitWhile(() => catSound.isPlaying);
        yield return new WaitForSeconds(1f);
        newMeow.Play();
        yield return new WaitWhile(() => newMeow.isPlaying);
        yield return new WaitForSeconds(1f);       
        exclamationMark.SetActive(true);
        newMeow.Play();
        yield return new WaitWhile(() => newMeow.isPlaying);
        yield return new WaitForSeconds(1f);
        
        changeScene.isChangeScene = true;
    }
}
