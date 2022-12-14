using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager instance = null;
    public Cauldron CauldronRef;
    [SerializeField] private GameObject[] _prefabsMinigame;
    [SerializeField] private Transform _minigamePos;
    [SerializeField] private GameObject youth;

    private GameObject _miniGame;
    public bool minigameSuccess;
    private string _recipeIDToSpawn;

    [SerializeField] private AudioClip _succesSound;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void SetMinigame(string _recipeID)
    {
        _recipeIDToSpawn = _recipeID;
        //Disable cursor
        Cursor.visible = false;
        minigameSuccess = false;

        _miniGame = null;
        //Load the correct prefab based on the selected minigame
        switch (_recipeID)
        {
            case "101":
                _miniGame = Instantiate(_prefabsMinigame[0], _minigamePos);
                break;

            case "102":
                _miniGame = Instantiate(_prefabsMinigame[1], _minigamePos);
                break;

            case "103":
                _miniGame = Instantiate(_prefabsMinigame[2], _minigamePos);
                break;

            case "104":
                _miniGame = Instantiate(_prefabsMinigame[3], _minigamePos);
                break;

            case "105":
                _miniGame = Instantiate(_prefabsMinigame[4], _minigamePos);
                break;

            case "106":
                _miniGame = Instantiate(_prefabsMinigame[5], _minigamePos);
                break;

            case "109":
                _miniGame = Instantiate(_prefabsMinigame[6], _minigamePos);
                break;

            case "110":
                _miniGame = Instantiate(_prefabsMinigame[7], _minigamePos);
                break;
            case "test":
                _miniGame = Instantiate(youth, _minigamePos);
                break;
        }
    }

    public void EndMiniGame(bool _succes, float _destroyTime)
    {
        Cursor.visible = true;
        if (_succes)
        {
            //award extra point, play sound(?), effect(?) etc
            minigameSuccess = true;
            TempAudioManager.instance.PlaySoundEffect(_succesSound);
            GameStats.instance.streakPoints += 1;
            GameStats.instance.RunStreakCheckpoint();
        }
        else
        {
            GameStats.instance.streakPoints = 0;
            GameStats.instance.RunStreakCheckpoint();
        }

        if(_recipeIDToSpawn != "test")
            CauldronRef.SpawnPotionAfterMinigame(_recipeIDToSpawn);

        Destroy(_miniGame, _destroyTime);
    }


}
