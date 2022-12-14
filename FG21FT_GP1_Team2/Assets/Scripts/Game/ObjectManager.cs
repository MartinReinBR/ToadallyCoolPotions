using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static List<GameObject> allIngredients;

    private void Start()
    {
        allIngredients = new List<GameObject>();
    }

    public static void addIngredientToList(GameObject newIngredient)
    {
        if (allIngredients.Count >= 20)
        {
            allIngredients.Insert(20, newIngredient);
            Destroy(allIngredients[0]);
            for (int i = 0; i < 20; i++)
            {
                allIngredients[i] = allIngredients[i + 1];
            }
        }

        else
        {
            allIngredients.Add(newIngredient);
        }
    }
}
