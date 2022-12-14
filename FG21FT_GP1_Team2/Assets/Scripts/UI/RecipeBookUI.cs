using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Made by Hadi
public class RecipeBookUI : MonoBehaviour
{
    [SerializeField] GameObject GameHandler;

    [SerializeField] private string id;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private Image recipeSprite;
    [SerializeField] private Image ingredientOneSprite;
    [SerializeField] private Image ingredientTwoSprite;
    
    [SerializeField] private bool unknownIngredientOne = false;
    [SerializeField] private bool unknownIngredientTwo = false;

    private Cauldron cauldron;

    private RecipeSprites recipeSprites;

    private void Awake()
    {
        cauldron = GameObject.Find("Cauldron").GetComponent<Cauldron>();
        
        cauldron.onPressCauldron += RefreshIngredients;
        recipeSprites = GameHandler.GetComponent<RecipeSprites>();

        name.text = GameData.Recipes[id].name;

        UpdateSprites(id);
    }

    private void OnDestroy()
    {
        cauldron.onPressCauldron -= RefreshIngredients;
    }

    public void RefreshIngredients(string receivedId)
    {
        if (receivedId == id)
        {
            unknownIngredientOne = false;
            unknownIngredientTwo = false;
        }
        
        UpdateSprites(id);
    }

    void UpdateSprites(string _id)
    {
        if (GameStats.instance.unlockedRecipes.Contains(_id))
        {
            recipeSprite.sprite = GameData.Recipes[_id].sprite;
        }
        else
        {
            recipeSprite.sprite = recipeSprites.GetUnknownRecipeSprite();
        }
        
        if (GameStats.instance.unlockedIngredients.Contains(GameData.Recipes[_id].ingredientsIds[0]) && !unknownIngredientOne)
        {
            ingredientOneSprite.sprite = GameData.Ingredients[GameData.Recipes[_id].ingredientsIds[0]].sprite;
        }
        else if (unknownIngredientOne && GameStats.instance.unlockedIngredients.Contains(GameData.Recipes[_id].ingredientsIds[0]))
        {
            ingredientOneSprite.sprite = recipeSprites.GetUnknownSprite();
        }
        else
        {
            ingredientOneSprite.sprite = recipeSprites.GetMissingSprite();
        }
        
        if (GameStats.instance.unlockedIngredients.Contains(GameData.Recipes[_id].ingredientsIds[1]) && !unknownIngredientTwo)
        {
            ingredientTwoSprite.sprite = GameData.Ingredients[GameData.Recipes[_id].ingredientsIds[1]].sprite;
        }
        else if (unknownIngredientTwo && GameStats.instance.unlockedIngredients.Contains(GameData.Recipes[_id].ingredientsIds[1]))
        {
            ingredientTwoSprite.sprite = recipeSprites.GetUnknownSprite();
        }
        else
        {
            ingredientTwoSprite.sprite = recipeSprites.GetMissingSprite();
        }
    }
}
