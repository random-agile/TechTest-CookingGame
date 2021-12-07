using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutFood : MonoBehaviour
{
    [SerializeField]
    private GameObject knife;
    private GameObject food;
    [SerializeField]
    private GameObject foodResult;
    [SerializeField]
    private GameObject slider;
    [SerializeField]
    private List<ParticleSystem> particles;
    [SerializeField]
    private Slider bar;
    private string foodType;
    private bool isPlaced;
    private bool isDone;


    void Start()
    {

    }

    public void CheckFood()
    {
        if(!isPlaced)
        {
            isPlaced = true;
        }
        else
        {
            bar.value = 0;
            isPlaced = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            food = other.gameObject;
            //food.SetActive(true);
            DetermineFoodType(other.gameObject.name);
        }

        if (other.gameObject.CompareTag("Knife") && isPlaced && !isDone)
        {
            Cut();
        }
    }

    //determine which type of ingredient is placed on the cutting board and set the number of cuts needed
    void DetermineFoodType(string name)
    {
        switch (name)
        {
            case "tomato":
                foodType = "Tomato";
                bar.maxValue = 6;
                break;
            case "onion":
                foodType = "Onion";
                bar.maxValue = 15;
                break;
            case "cheese":
                foodType = "Cheese";
                bar.maxValue = 6;
                break;
            case "cabbage":
                foodType = "Cabbage";
                bar.maxValue = 8;
                break;
            case "loaf":
                foodType = "Loaf";
                bar.maxValue = 4;
                break;
            case "wholeHam":
                foodType = "WholeHam";
                bar.maxValue = 6;
                break;
            case "turkey":
                foodType = "Turkey";
                bar.maxValue = 10;
                break;
            case "avocado":
                foodType = "Avocado";
                bar.maxValue = 6;
                break;
        }
    }

    // play every sounds/fx when a cutting happen
    void Cut()
    {
        if(bar.value >= bar.maxValue -1)
        {
            particles[1].Play();
            SoundManagerScript.PlaySound("Finish");
            bar.value = 0;
            food.SetActive(false);
            DetermineResult(foodType);
            slider.SetActive(false);
            //isDone = true;
        }
        else
        {
            slider.SetActive(true);
            bar.value++;
            particles[0].Play();
            SoundManagerScript.PlaySound("Cut");
        }
    }

    // determine which ingredient was cut to instantiate a sliced version of it
    void DetermineResult(string foodType)
    {
        switch (foodType)
        {
            case "Tomato":
                Instantiate(foodResult.transform.GetChild(0), new Vector3(-91.39f,-19.535f,48.534f), Quaternion.identity);
                break;
            case "Onion":
                Instantiate(foodResult.transform.GetChild(1), new Vector3(1.692f,0.8031f,0.617f), Quaternion.identity);
                break;
            case "Cheese":
                Instantiate(foodResult.transform.GetChild(2), new Vector3(0,0,0), Quaternion.identity);
                break;
            case "Cabbage":
                Instantiate(foodResult.transform.GetChild(3), new Vector3(1.692f,0.8031f,0.617f), Quaternion.identity);
                break;
            case "Loaf":
                Instantiate(foodResult.transform.GetChild(4), new Vector3(1.692f,0.8031f,0.617f), Quaternion.identity);
                break;
            case "WholeHam":
                Instantiate(foodResult.transform.GetChild(5), new Vector3(-91.39f,-19.568f,48.534f), Quaternion.identity);
                break;
            case "Turkey":
                Instantiate(foodResult.transform.GetChild(6), new Vector3(1.692f,0.8031f,0.617f), Quaternion.identity);
                break;
            case "Avocado":
                Instantiate(foodResult.transform.GetChild(7), new Vector3(1.692f,0.8031f,0.617f), Quaternion.identity);
                break;
        }
    }

    public void GrabKnifeSFX()
    {
        SoundManagerScript.PlaySound("GrabKnife");
    }

    public void GrabFoodSFX()
    {

    }

}
