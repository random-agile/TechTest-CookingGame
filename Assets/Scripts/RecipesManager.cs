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
    [SerializeField] private GameObject foodLevel;
    [SerializeField] private GameObject cookButton;
    [SerializeField] private Animation anim;
    private int x;

    void Start()
    {
       foodManager = GameObject.Find("FoodManager").GetComponent<FoodManager>();
       dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
    }

    //ensure that only sliced ingredients can be put in the magic pot
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
        if(ingredients.Count >= 4 && foodManager.isDone && isSliced)
        {
        SoundManager.PlaySound("PutIngredient");
        DestroyUsedIngredients();
        foodManager.particles[3].Play();
        foodManager.isDone = false;
        CheckRecipes();
        }

        else if(foodManager.isDone && isSliced)
        {
        SoundManager.PlaySound("PutIngredient");
        ingredients.Add(foodManager.foodType);
        foodLevel.SetActive(true);
        foodManager.particles[3].Play();
        foodManager.isDone = false;
        DestroyUsedIngredients();
        if(ingredients.Count >= 3){cookButton.SetActive(true);}
        }
    }   

    //destroy all instanciated prefabs
    void DestroyUsedIngredients()
    {
        var clones = GameObject.FindGameObjectsWithTag("Clone");
            foreach (var clone in clones)
            {   
                clone.transform.position = new Vector3(-991.39f,-199.535f,498.534f);
                Destroy(clone);
            }
    }

    //check what was put inside the magic pot, to determine the recipe and amount of points obtained
    public void CheckRecipes()
    {
        if(ingredients.Contains("Tomato") && ingredients.Contains("Onion") && ingredients.Contains("Loaf") && ingredients.Contains("Ham"))
        {    
            x = 0;
            StartCoroutine(WaitAnim());
            dataManager.score += 2000;           
        }
        else if(ingredients.Contains("Loaf") && ingredients.Contains("Cheese") && ingredients.Contains("Ham"))
        {
            x = 1;
            StartCoroutine(WaitAnim());
            dataManager.score += 1000;
        }
        else if(ingredients.Contains("Tomato") && ingredients.Contains("Onion") && ingredients.Contains("Cabbage") && ingredients.Contains("Avocado"))
        {
            x = 2;
            StartCoroutine(WaitAnim());
            dataManager.score += 2000;
        }
        else if(ingredients.Contains("Onion") && ingredients.Contains("Cabbage") && ingredients.Contains("Avocado"))
        {
            x = 3;
            StartCoroutine(WaitAnim());
            dataManager.score += 1000;
        }
        else if(ingredients.Contains("Turkey") && ingredients.Contains("Tomato") && ingredients.Contains("Avocado") && ingredients.Contains("Cheese"))
        {
            x = 4;
            StartCoroutine(WaitAnim());
            dataManager.score += 3000;
        }
        else
        {
            x = 5;
            StartCoroutine(WaitAnim());
            dataManager.score += 500;
        }
    }

    //abstracting lines of code from CheckRecipes(), play VFX, SFX, anims & clear list
    void AbstractRecipes()
    {
        SoundManager.PlaySound("CookingDone");
        meals[x].SetActive(true);
        foodManager.particles[2].Play();
        ingredients.Clear();
    }

    IEnumerator WaitForDish()
    {
        yield return new WaitForSeconds(3f);
        meals[x].SetActive(false);
        foodManager.particles[3].Stop();
        foodLevel.SetActive(false);
    }

    IEnumerator WaitAnim()
    {
        SoundManager.PlaySound("CookingIn");
        cookButton.SetActive(false);
        anim.Play();
        yield return new WaitForSeconds(3.5f);
        AbstractRecipes();
        StartCoroutine(WaitForDish());
    }
}
