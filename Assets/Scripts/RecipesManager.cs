using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RecipesManager : MonoBehaviour
{
    FoodManager foodManager;
    DataManager dataManager;
    [SerializeField] private List<GameObject> meals;
    [SerializeField] private List<string> ingredients;
    private bool isSliced;
    [SerializeField] XRSocketInteractor potSocket;

    void Start()
    {
        foodManager = GameObject.Find("FoodManager").GetComponent<FoodManager>();
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Clone"))
        {
            potSocket.enabled = true;
            isSliced = true;
        }
        else if(!other.gameObject.CompareTag("Clone"))
        {
            potSocket.enabled = false;
        }
    }

    void OnCollisionExit(Collision other)
    {
        isSliced = false;
    }

    //add ingredient to the magic pot and destroy all instantiated prefabs
    public void AddIngredients()
    {
        //if(ingredients.Count >= 6 && foodManager.isDone && isSliced)
        //{

        //}
        if(foodManager.isDone && isSliced)
        {
        ingredients.Add(foodManager.foodType);
        foodManager.isDone = false;
        var clones = GameObject.FindGameObjectsWithTag("Clone");
            foreach (var clone in clones)
            {   
                clone.transform.position = new Vector3(-991.39f,-199.535f,498.534f);
                Destroy(clone);
            }
        }   
    }

    //check what was put inside the magic pot, to determine the recipe and amount of points obtained then clear the list
    void CheckRecipes()
    {
        if(ingredients.Contains("Tomato") && ingredients.Contains("Onion") && ingredients.Contains("Cabbage") && ingredients.Contains("Loaf") && ingredients.Contains("Ham"))
        {
            meals[0].SetActive(true);
            StartCoroutine(WaitForDish());
            meals[0].SetActive(false);
            dataManager.score += 2000;           
            ingredients.Clear();
        }
        else if(ingredients.Contains("Loaf") && ingredients.Contains("Cheese") && ingredients.Contains("Ham"))
        {
            meals[1].SetActive(true);
            StartCoroutine(WaitForDish());
            meals[1].SetActive(false);
            dataManager.score += 1000;
            ingredients.Clear();
        }
        else if(ingredients.Contains("Tomato") && ingredients.Contains("Onion") && ingredients.Contains("Cabbage") && ingredients.Contains("Avocado"))
        {
            meals[2].SetActive(true);
            StartCoroutine(WaitForDish());
            meals[2].SetActive(false);
            dataManager.score += 2000;
            ingredients.Clear();
        }
        else if(ingredients.Contains("Onion") && ingredients.Contains("Cabbage") && ingredients.Contains("Avocado"))
        {
            meals[3].SetActive(true);
            StartCoroutine(WaitForDish());
            meals[3].SetActive(false);
            dataManager.score += 1000;
            ingredients.Clear();
        }
        else if(ingredients.Contains("Turkey") && ingredients.Contains("Tomato") && ingredients.Contains("Avocado") && ingredients.Contains("Cheese"))
        {
            meals[4].SetActive(true);
            StartCoroutine(WaitForDish());
            meals[4].SetActive(false);
            dataManager.score += 3000;
            ingredients.Clear();
        }
        else
        {
            meals[5].SetActive(true);
            StartCoroutine(WaitForDish());
            meals[5].SetActive(false);
            dataManager.score += 500;
            ingredients.Clear();
        }
    }

    IEnumerator WaitForDish()
    {
        yield return new WaitForSeconds(3f);
    }
}
