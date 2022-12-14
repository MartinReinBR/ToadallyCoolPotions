using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Made by Hadi
public class RecipeSprites : MonoBehaviour
{
    [SerializeField] private Sprite unknownRecipeSprite;
    [SerializeField] private Sprite unknownSprite;
    [SerializeField] private Sprite missingSprite;

    public Sprite GetMissingSprite()
    {
        return missingSprite;
    }
    public Sprite GetUnknownRecipeSprite()
    {
        return unknownRecipeSprite;
    }
    
    public Sprite GetUnknownSprite()
    {
        return unknownSprite;
    }
}
