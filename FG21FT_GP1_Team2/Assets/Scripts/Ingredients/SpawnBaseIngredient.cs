using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBaseIngredient : MonoBehaviour
{
    public List<GameObject> listOfPotions;
    [SerializeField] private Cauldron cauldron;
    [SerializeField] private PopupMessagePrefab popupMessage;
    public static SpawnBaseIngredient instance;
    private bool hasDisplayedTutorialMessage = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
    }

    public void SpawnExtraIngredients()
    {
        if (hasDisplayedTutorialMessage) return;
        hasDisplayedTutorialMessage = true;
        PlayerPrefs.SetInt("TutorialComplete", 1);
        PlayerPrefs.Save();
        Instantiate(listOfPotions[0], transform.position + new Vector3(0,0.1f,0), transform.rotation);
        Instantiate(listOfPotions[1], transform.position + new Vector3(2.4f,0,0f), transform.rotation);
        GameStats.instance.unlockedIngredients.Add("004");
        GameStats.instance.unlockedIngredients.Add("005");
        PopupMessageManager.instance.RequestDisplayPopupMessage(popupMessage.popupMessage);

        if (cauldron.onPressCauldron != null)
        {
            cauldron.onPressCauldron.Invoke("777");
        }
    }

}
