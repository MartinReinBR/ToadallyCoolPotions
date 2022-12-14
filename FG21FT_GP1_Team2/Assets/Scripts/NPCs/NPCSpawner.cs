using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public static NPCSpawner instance = null;

    public GameObject baseIngredientSpawner;
    private bool ingredientsSpawned = false;

    [SerializeField] public List<GameObject> npcPrefabs = new List<GameObject>();
    //public GameObject baseIngredientSpawner;

    private GameObject _npc;
    private string _questID;
    private Vector3 _npcStartingPos = new Vector3(21f, 10.5f, 5.5f);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SpawnNPC();
    }

    public void SpawnNPC()
    {
        SelectRandomNPC();

        if (GameStats.instance.tutorialStage == 1)
        {
            _questID = "101";
        }

        else if (GameStats.instance.tutorialStage == 2)
        {
            _questID = "102";
        }
        
        else
        {
            SelectRandomQuest();
            if (!ingredientsSpawned)
            {
                baseIngredientSpawner.GetComponent<SpawnBaseIngredient>().SpawnExtraIngredients();
                ingredientsSpawned = true;
            }
        }

        GameObject SpawnedNPC = Instantiate(_npc, _npcStartingPos, Quaternion.identity);
        SpawnedNPC.GetComponent<QuestSystem>().questID = _questID;
    }

    private void SelectRandomQuest()
    {
        int randomIndex = Random.Range(101,GameData.Recipes.Count + 101);
        _questID = GameData.Recipes[randomIndex.ToString()].id;
    }

    private void SelectRandomNPC()
    {
        int randomIndex = Random.Range(0, npcPrefabs.Count);
        _npc = npcPrefabs[randomIndex];
    }
}
