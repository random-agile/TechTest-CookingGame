using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutFood : MonoBehaviour
{
    [SerializeField]
    private GameObject knife;
    [SerializeField]
    private GameObject food;
    [SerializeField]
    private GameObject foodResult;
    [SerializeField]
    private GameObject slider;
    [SerializeField]
    private List<ParticleSystem> particles;
    [SerializeField]
    private Slider bar;
    private bool isPlaced;
    private bool isDone;

    void Start()
    {
        bar.maxValue = 10;
        slider.SetActive(false);
    }

    public void CheckFood()
    {
        if(!isPlaced)
        {
            isPlaced = true;
        }
        else
        {
            isPlaced = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Knife") && isPlaced && !isDone)
        {
            Cut();
        }
    }

    // play every sounds/fx when a cutting happen
    void Cut()
    {
        if(bar.value >= 10)
        {
            particles[1].Play();
            SoundManagerScript.PlaySound("Finish");
            bar.value = 0;
            food.SetActive(false);
            foodResult.SetActive(true);
            slider.SetActive(false);
            isDone = true;
        }
        else
        {
            slider.SetActive(true);
            bar.value++;
            particles[0].Play();
            SoundManagerScript.PlaySound("Cut");
        }
    }

    public void KnifeGrab()
    {
        SoundManagerScript.PlaySound("GrabKnife");
    }

}
