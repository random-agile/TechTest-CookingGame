using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip cut, finish, grabKnife, cookingDone, grabFood, putIngredient, cookingIn;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Awake()
    {
        cut = Resources.Load<AudioClip>("Cut");
        finish = Resources.Load<AudioClip>("Finish");
        grabKnife = Resources.Load<AudioClip>("GrabKnife");
        cookingDone = Resources.Load<AudioClip>("CookingDone");
        grabFood = Resources.Load<AudioClip>("GrabFood");
        putIngredient = Resources.Load<AudioClip>("PutIngredient");
        cookingIn = Resources.Load<AudioClip>("CookingIn");

        audioSrc = GetComponent<AudioSource>();
    }    

    //the main switch/case that will be used to trigger sound effects
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Cut":
                audioSrc.PlayOneShot(cut, 2f);
                break;
            case "Finish":
                audioSrc.PlayOneShot(finish, 1.5f);
                break;
            case "GrabKnife":
                audioSrc.PlayOneShot(grabKnife, 2f);
                break;
            case "CookingDone":
                audioSrc.PlayOneShot(cookingDone, 2f);
                break;
            case "GrabFood":
                audioSrc.PlayOneShot(grabFood, 2f);
                break;
            case "PutIngredient":
                audioSrc.PlayOneShot(putIngredient, 2f);
                break;
            case "CookingIn":
                audioSrc.PlayOneShot(cookingIn, 2f);
                break;
        }
    }

    public void GrabKnifeSFX()
    {
        PlaySound("GrabKnife");
    }

    public void GrabFoodSFX()
    {
        PlaySound("GrabFood");
    }
    
}
