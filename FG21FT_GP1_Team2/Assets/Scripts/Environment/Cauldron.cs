using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public AudioClip bubbleSound;
    public ParticleSystem BoilEffect;
    public delegate void OnPressCauldron(string id);
    public OnPressCauldron onPressCauldron;

    public GameObject baseIngredientSpawner;
    public GameObject PotionSpawner;
    public GameObject CauldronActivationIndicator;

    private string _id1 = "";
    private string _id2 = "";
    
    void Start()
    {
        CauldronActivationIndicator.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ingredient"))
        {
            ManageIngredients(other.GetComponent<TempIngredient>().IngredientID);

            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Potion"))
        {
            Destroy(other.gameObject);
        }
    }

    private void ManageIngredients(string ingredientID)
    {
        if (_id1 == "")
        {
            _id1 = ingredientID;
        }
        else if (_id2 == "")
        {
            _id2 = ingredientID;
        }

        if(CauldronActivationIndicator != null) //TEMPORARY CHECK IF THERE IS AN INDICATOR
        {
            if (ContainsTwoIngredients())
            {
                CauldronActivationIndicator.SetActive(true);
            }
            else
            {
                CauldronActivationIndicator.SetActive(false);
            }
        }
    }

    public void ResetCouldron()
    {
        _id1 = "";
        _id2 = "";
        CauldronActivationIndicator.SetActive(false); //Dave added
    }

    public void CraftPotion()
    {
        bool isCrafted =  false;

        if(!isCrafted && _id1 != "" && _id2 != "") //Dave added
        {
            TempAudioManager.instance.PlaySoundEffect(bubbleSound);

            //Size up the boil effect
            var _ParticleMain = BoilEffect.main;
            var _ParticleEmission = BoilEffect.emission;
            _ParticleMain.startLifetime = 3f;
            _ParticleEmission.rateOverTime = 15f;
        }
        else
        {
            //Play klunk sound effect
        }

        for (int i = 101; i < GameData.Recipes.Count + 101; i++)
        {
            if(GameData.Recipes[i.ToString()].ingredientsIds.Contains(_id1) && GameData.Recipes[i.ToString()].ingredientsIds.Contains(_id2) && _id1 != _id2)
            {
                MinigameManager.instance.SetMinigame(GameData.Recipes[i.ToString()].id);

                if (!GameStats.instance.unlockedRecipes.Contains(GameData.Recipes[i.ToString()].id))
                {
                    GameStats.instance.unlockedRecipes.Add(GameData.Recipes[i.ToString()].id);

                    if (GameStats.instance.AllUnlocked())
                    {
                        Debug.Log("Everything unlocked!");
                    }

                    if (onPressCauldron != null)
                    {
                        onPressCauldron.Invoke(GameData.Recipes[i.ToString()].id);
                    }
                }

                isCrafted = true;
            }       
        }

        if(!isCrafted && _id1 != "" && _id2 != "")
        {
            PotionSpawner.GetComponent<SpawnPotion>().SpawnSpecificPotion("666");
            TempAudioManager.instance.PlaySoundEffect(bubbleSound); //Dave added
        }

        if (_id1 != "" && _id2 != "")
        {
            ResetCouldron();
        }

    }

    public void SpawnPotionAfterMinigame(string _id)
    {
        PotionSpawner.GetComponent<SpawnPotion>().SpawnSpecificPotion(_id);

        //Size down the particle effect
        var _ParticleMain = BoilEffect.main;
        var _ParticleEmission = BoilEffect.emission;
        _ParticleMain.startLifetime = 2f;
        _ParticleEmission.rateOverTime = 10f;
    }

    private bool ContainsTwoIngredients() //Dave added
    {
        if(_id1 != "" && _id2 != "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
