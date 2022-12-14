using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempIngredient : MonoBehaviour
{
    [SerializeField] private string _ingredientID = "";
    [SerializeField] private string _ingredientName = "";

    private void Start()
    {
        _ingredientName = GameData.Ingredients[_ingredientID].name;
        ObjectManager.addIngredientToList(gameObject);

    }

    public string IngredientID
    {
        get
        {
            return this._ingredientID;
        }
        set
        {
            _ingredientID = value;
        }
    }

    public string IngredientName
    {
        get
        {
            return this._ingredientName;
        }
        set
        {
            _ingredientName = value;
        }
    }
}
