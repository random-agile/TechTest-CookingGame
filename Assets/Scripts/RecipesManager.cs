using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesManager : MonoBehaviour
{
    FoodManager foodManager;
    private List<string> ingredients;

    void Start()
    {
        foodManager = GameObject.Find("FoodManager").GetComponent<FoodManager>();
    }

    //add ingredient to the magic pot and destroy all instantiated prefabs
    void AddIngredients()
    {
        if(ingredients.Count >= 6 && foodManager.isDone)
        {

        }
        else if(foodManager.isDone)
        {
        ingredients.Add(foodManager.foodType);
        foodManager.isDone = false;
        var clones = GameObject.FindGameObjectsWithTag ("Clone");
        foreach (var clone in clones)
            {Destroy(clone);}
        }   
    }

    //check what was put inside the magic pot, to determine the recipe and amount of points obtained then clear the list
    void CheckRecipes()
    {
        if(ingredients.Contains("Tomato") && ingredients.Contains("Onion") && ingredients.Contains("Cabbage") && ingredients.Contains("Loaf"))
        {
            ingredients.Clear();
        }
    }
}
