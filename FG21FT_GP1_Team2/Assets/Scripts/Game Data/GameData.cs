using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;

//Made by Hadi
public class GameData
{
    static string ingredientsDatabasePath = Application.streamingAssetsPath + "/Databases/IngredientsDatabase.txt";
    static string recipeDatabasePath = Application.streamingAssetsPath + "/Databases/RecipesDatabase.txt";

    //GameData is a system that uses Dictionaries, it takes 2 parameters. Class and Key (Id).

    //Create public static dictionaries
    //Example: public static Dictionary<string, Ship> Ships;
    public static Dictionary<string, Ingredient> Ingredients;
    public static Dictionary<string, Recipe> Recipes;

    public static void Load() //This method is similar to Start(). It has to be loaded from another script (forexample BoardController)
    {
        //Initialize in this method
        //Example: Ships = new Dictionary<string, Ship>();

        Ingredients = new Dictionary<string, Ingredient>();
        Recipes = new Dictionary<string, Recipe>();
        IngredientsGenerator();
        RecipesGenerator();

        //Call methods
        //Example:ShipGenerator();
    }

    static void IngredientsGenerator()
    {
        //This method splits the database text into enteries and assigns them as data into the Creator method.
        //NOTE: It is very important that data is assigned in correct order

        Sprite[] ingSprites = Resources.LoadAll<Sprite>("Sprites/IngredientsIcons");
        
        string[] ingredientsDatabaseLines = File.ReadAllLines(ingredientsDatabasePath);

        for(int i = 1; i < ingredientsDatabaseLines.Length; i++) // skip first line
        {
            Sprite sprite = ingSprites[i - 1];
            
            string[] enteries = ingredientsDatabaseLines[i].Split('_');
            
            IngredientCreator(enteries[0], enteries[1], sprite);
        }
    }
    
    static void RecipesGenerator()
    {
        Sprite[] recSprites = Resources.LoadAll<Sprite>("Sprites/RecipesIcons");

        string[] recipesDatabaseLines = File.ReadAllLines(recipeDatabasePath);

        for(int i = 1; i < recipesDatabaseLines.Length; i++) // skip first line
        {
            string[] enteries = recipesDatabaseLines[i].Split('_');
            List<string> ingredientsIds = new List<string>();
            
            Sprite sprite = recSprites[i - 1];
                        
            if (enteries.Length > 2)
            {
                for (int j = 3; j < enteries.Length; j++)
                {
                    ingredientsIds.Add(enteries[j]);
                }
            }

            RecipeCreator(enteries[0], enteries[1], enteries[2], ingredientsIds, sprite);
        }
    }
    
    //CREATOR
    static void IngredientCreator(string id, string ingredientName, Sprite sprite)
    {
        Ingredient ingredient = new Ingredient();
        ingredient.id = id;
        ingredient.name = ingredientName;
        ingredient.sprite = sprite;
        
        Ingredients.Add(id, ingredient);
    }
    
    static void RecipeCreator(string id, string recipeName, string recipeDescription, List<string> ingredientsIndexes, Sprite sprite)
    {
        Recipe recipe = new Recipe();
        recipe.id = id;
        recipe.name = recipeName;
        recipe.description = recipeDescription;
        recipe.ingredientsIds = ingredientsIndexes;
        recipe.sprite = sprite;

        Recipes.Add(id, recipe);
    }
    //GameData.Recipes["101"].name; EXAMPLE on how to reference to the database
}
