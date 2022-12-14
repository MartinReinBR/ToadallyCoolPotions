using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSingleIngredient : MonoBehaviour
{
    public GameObject IngredientToSpawn;

    private void OnMouseDown()
    {
        Instantiate(IngredientToSpawn, transform.position + new Vector3(0,2f,-0.5f), transform.rotation);
    }
}
