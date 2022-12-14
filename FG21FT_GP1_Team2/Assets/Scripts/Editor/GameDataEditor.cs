using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using NUnit.Framework;

//Made by Hadi
[CustomEditor(typeof(GameData))]
public class GameDataEditor : EditorWindow
{
    private float spaceBetweenGUI = 10;
    string ingredientsDatabasePath = Application.streamingAssetsPath + "/Databases/IngredientsDatabase.txt";
    string recipeDatabasePath = Application.streamingAssetsPath + "/Databases/RecipesDatabase.txt";
    
    // Ingredients Variables
    static string ingredientId;
    static string ingredientName;
    
    // Recipe Variables
    static string recipeId;
    private static string recipeName;
    private static string recipeDescription;
    
    static int[] listIndex;
    static string[] recipeIngredientsIds;
    int ingredientCount;
    static int ingredientMaxCount = 5;

    [MenuItem("Window/Game Data")]
    public static void ShowWindow()
    {
        listIndex = new int[ingredientMaxCount];
        GameData.Load();
        
        GetWindow<GameDataEditor>("Add Data");
    }

    void OnGUI()
    {
        GUILayout.Label("New Ingredient", EditorStyles.boldLabel);
        ingredientName = EditorGUILayout.TextField("Ingredient Name", ingredientName);
        GUILayout.Label("Note: Make sure that the id you're assigning doesn't already exist!", EditorStyles.label);
        GUILayout.Label("Last Id entery in database: " + GameData.Ingredients.Count.ToString().PadLeft(3, '0'), EditorStyles.label);
        GUILayout.Space(spaceBetweenGUI);
        
        ingredientId = EditorGUILayout.TextField("Ingredient ID", ingredientId);

        if(GUILayout.Button("Add New Ingredient"))
        {
            WriteIngredientString(ingredientsDatabasePath);
        }
        
        GUILayout.Space(spaceBetweenGUI);
        GUILayout.Label("New Recipe", EditorStyles.boldLabel);
        GUILayout.Label("Last Id entery in database: 1" + GameData.Recipes.Count.ToString().PadLeft(2, '0'), EditorStyles.label);
        GUILayout.Space(spaceBetweenGUI);
        
        recipeId = EditorGUILayout.TextField("Recipe ID", recipeId);
        recipeName = EditorGUILayout.TextField("Recipe Name", recipeName);
        recipeDescription = EditorGUILayout.TextField("Recipe Description", recipeDescription);
        ingredientCount = EditorGUILayout.IntSlider("Ingredients Amount", ingredientCount, 0, ingredientMaxCount);

        for (int i = 0; i < ingredientCount; i++)
        {
            int ingredientTextCount = i + 1;
            listIndex[i] = EditorGUILayout.Popup("Ingredient " + ingredientTextCount, listIndex[i], CreateIngredientsList());
        }
        
        if(GUILayout.Button("Add New Recipe"))
        {
            recipeIngredientsIds = new string[ingredientCount];
            
            for (int i = 0; i < ingredientCount; i++)
            {
                int intId = listIndex[i] + 1;
                string id = intId.ToString().PadLeft(3, '0');

                recipeIngredientsIds[i] = id;
            }
            WriteRecipesString(recipeDatabasePath);
        }
    }

    static void WriteIngredientString(string path)
    {
        //Write some text to the path.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.Write("\n" + ingredientId + "_" + ingredientName);
        writer.Close();

        ingredientId = "";
        ingredientName = "";
    }
    
    static void WriteRecipesString(string path)
    {
        StreamWriter writer = new StreamWriter(path, true);
        writer.Write("\n" + recipeId + "_" + recipeName + "_" + recipeDescription + listOfIndexes());
        writer.Close();

        string listOfIndexes()
        {
            string text = "";
            for (int i = 0; i < recipeIngredientsIds.Length; i++)
            {
                text += "_" + recipeIngredientsIds[i];
            }

            return text;
        }
    }

    static string[] CreateIngredientsList()
    {
        string[] items = new string[GameData.Ingredients.Count];

        for (int i = 0; i < items.Length; i++)
        {
            int idInDictionary = i + 1;
            items[i] = GameData.Ingredients[idInDictionary.ToString().PadLeft(3, '0')].name;
        }

        return items;
    }
}
