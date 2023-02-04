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

    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetButtonDown("Meow") && !catSound.isPlaying) {
            StartCoroutine(CatMeow());
            numberOfMeows++;
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
        rippleEffectMom.Play();
        momSound.Play();
    }
}
