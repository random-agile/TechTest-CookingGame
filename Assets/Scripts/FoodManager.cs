using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodManager : MonoBehaviour
{
    [SerializeField] private GameObject knife;
    private GameObject food;
    [SerializeField] private GameObject foodResult;
    [SerializeField] private GameObject slider;
    [SerializeField] private Slider cutBar;
    [SerializeField] private Slider knifeBar;
    public List<ParticleSystem> particles;

    public string foodType;
    private bool isPlaced;
    public bool isDone;

    //ensure the food is placed on the socket and reset cut slider if player remove the item before finishing
    public void CheckFood()
    {
        if(!isPlaced)
        {
            isPlaced = true;
        }
        else
        {
            cutBar.value = 0;
            isPlaced = false;
        }
    }

    // get data when placing a new ingredient on the cutting board or cut it if the knife is used
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            food = other.gameObject;
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
                cutBar.maxValue = 12;
                break;
            case "onion":
                foodType = "Onion";
                cutBar.maxValue = 30;
                break;
            case "cheese":
                foodType = "Cheese";
                cutBar.maxValue = 12;
                break;
            case "cabbage":
                foodType = "Cabbage";
                cutBar.maxValue = 16;
                break;
            case "loaf":
                foodType = "Loaf";
                cutBar.maxValue = 8;
                break;
            case "wholeHam":
                foodType = "Ham";
                cutBar.maxValue = 12;
                break;
            case "turkey":
                foodType = "Turkey";
                cutBar.maxValue = 20;
                break;
            case "avocado":
                foodType = "Avocado";
                cutBar.maxValue = 12;
                break;
        }
    }

    // trigger when the cutting happens and play every sounds/fx along it
    void Cut()
    {
        if(cutBar.value >= cutBar.maxValue -1)
        {
            particles[1].Play();
            SoundManager.PlaySound("Finish");
            cutBar.value = 0;
            food.SetActive(false);
            food = null;
            knifeBar.value += 10;
            DetermineResult(foodType);
            slider.SetActive(false);
            isDone = true;
        }
        else
        {
            slider.SetActive(true);
            cutBar.value++;
            particles[0].Play();
            SoundManager.PlaySound("Cut");
        }
    }

    // determine which ingredient was cut to instantiate a sliced version of it
    void DetermineResult(string foodType)
    {
        switch (foodType)
        {
            case "Tomato":
                Instantiate(foodResult.transform.GetChild(0), new Vector3(1.665f,1f,0.69f), Quaternion.identity);
                break;
            case "Onion":
                Instantiate(foodResult.transform.GetChild(1), new Vector3(1.665f,1f,0.69f), Quaternion.identity);
                break;
            case "Cheese":
                Instantiate(foodResult.transform.GetChild(2), new Vector3(1.665f,1f,0.69f), Quaternion.identity);
                break;
            case "Cabbage":
                Instantiate(foodResult.transform.GetChild(3), new Vector3(1.665f,1f,0.69f), Quaternion.identity);
                break;
            case "Loaf":
                Instantiate(foodResult.transform.GetChild(4), new Vector3(1.665f,1f,0.69f), Quaternion.identity);
                break;
            case "Ham":
                Instantiate(foodResult.transform.GetChild(5), new Vector3(1.665f,1f,0.69f), Quaternion.identity);
                break;
            case "Turkey":
                Instantiate(foodResult.transform.GetChild(6), new Vector3(1.665f,1f,0.69f), Quaternion.identity);
                break;
            case "Avocado":
                Instantiate(foodResult.transform.GetChild(7), new Vector3(1.665f,1f,0.69f), Quaternion.identity);
                break;
        }
    }
}
