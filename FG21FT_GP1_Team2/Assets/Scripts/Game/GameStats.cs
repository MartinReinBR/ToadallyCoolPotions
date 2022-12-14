using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats instance = null;

    [HideInInspector] public int customerCount;
    [HideInInspector] public int tutorialStage;
    [HideInInspector] public int points;
    [HideInInspector] public int streakPoints;
    public int checkpointValue;
    public int streakCheckpointValue;
    public List<string> unlockedIngredients;
    public List<string> unlockedRecipes;
    public List<string> tempRecepies;
    public List<string> tempIngredients;
    public AudioClip checkpointSound;
    [SerializeField] private GameObject _ESOTY;

    [SerializeField] private PopupMessagePrefab popupMessagePrefab;
    [SerializeField] private PopupMessagePrefab streakPopupMessage;
    private int nextCheckpoint;
    private int nextStreakCheckpoint;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        unlockedIngredients = new List<string>() {"001","002","003","006"};
        unlockedRecipes = new List<string>();
        tempRecepies = unlockedRecipes;
        tempIngredients = unlockedIngredients;

        customerCount = 0;
        points = 0;
        tutorialStage = 1;
        nextCheckpoint = checkpointValue;
        nextStreakCheckpoint = streakCheckpointValue;
    }

    private void Update()
    {
        tempRecepies = unlockedRecipes;
        tempIngredients = unlockedIngredients;
    }

    public void RunCheckpoint()
    {
        if (points >= nextCheckpoint)
        {
            TempAudioManager.instance.PlaySoundEffect(checkpointSound);
            PopupMessageManager.instance.RequestDisplayPopupMessage(new PopupMessage(popupMessagePrefab.popupMessage.icon, "", $"Well done, you reached {nextCheckpoint} points!"));
            nextCheckpoint += checkpointValue;
        }

    }

    public void RunStreakCheckpoint()
    {
        if (streakPoints == 0)
        {
            nextStreakCheckpoint = streakCheckpointValue;
            InGameUI.instance.customerCountTextObject.SetActive(false);
            InGameUI.instance.customerIcon.SetActive(false);
        }
        else if (streakPoints == 3)
        {
            InGameUI.instance.customerCountTextObject.SetActive(true);
            InGameUI.instance.customerIcon.SetActive(true);
        }
        else if (streakPoints >= nextStreakCheckpoint)
        {
            PopupMessageManager.instance.RequestDisplayPopupMessage(new PopupMessage(streakPopupMessage.popupMessage.icon, "", $"A streak of {nextStreakCheckpoint} perfect potions!"));
            nextStreakCheckpoint += streakCheckpointValue;
        }

        InGameUI.instance.UpdateStreak();
    }

    public bool AllUnlocked()
    {
        if(tempRecepies.Count == 6)
            return true;
        else
            return false;
    }

}
