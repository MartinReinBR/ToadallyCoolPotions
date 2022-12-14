using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public string _potionID = "";
    [SerializeField] private string _potionName = "";

    private void Start()
    {
        if(_potionID != "666")
        {
            _potionName = GameData.Recipes[_potionID].name;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            collision.gameObject.GetComponent<QuestSystem>().CompareIDs(_potionID);
            Destroy(gameObject);
        }
    }
}

