using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catMeow : MonoBehaviour
{
    public AudioSource catSound;
    public AudioSource momSound;
    public ParticleSystem rippleEffect;
    public ParticleSystem rippleEffectMom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Meow") && !catSound.isPlaying) {
            StartCoroutine(CatMeow());

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
