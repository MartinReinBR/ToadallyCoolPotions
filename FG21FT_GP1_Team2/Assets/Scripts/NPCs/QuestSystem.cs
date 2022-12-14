using UnityEngine;
using UnityEngine.Events;

public class QuestSystem : MonoBehaviour
{
    public string questID;
    public AudioClip[] pointSounds = new AudioClip[4];
    public Sprite angrySprite;
    
    private int _rewardPoints;
    private string _potionDescription;
    private bool _questComplete;
    private ChatBubble _chatBubble;
    private SpriteRenderer _npcSpriteRenderer;
    private InGameUI _gameUI;

    void Start()
    {
        _chatBubble = gameObject.GetComponentInChildren<ChatBubble>();
        _npcSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _gameUI = FindObjectOfType<InGameUI>();
        _rewardPoints = 3;

        _potionDescription = GameData.Recipes[questID].description;
        SetQuestChat();
    }

    public void SetQuestChat()
    {
        _chatBubble.SetupChat(ChatBubble.IconType.Happy, _potionDescription);
    }

    public void CompareIDs(string potionID)
    {
        if (potionID == questID)
        {
            _questComplete = true;

            if (GameStats.instance.tutorialStage <= 2)
            {
                GameStats.instance.tutorialStage++;
            }

            int _soundIndex = _rewardPoints;
            if (MinigameManager.instance.minigameSuccess)
            {
                _rewardPoints++;
            }
            
            TempAudioManager.instance.PlaySoundEffect(pointSounds[_soundIndex]);
            SetResult(_questComplete, _rewardPoints);
        }
        else
        {
            _rewardPoints -= 1;
            if (_rewardPoints == -1)
            {
                _questComplete = false;
                SetResult(_questComplete, _rewardPoints);
            }
            else if (_rewardPoints == 0)
            {
                _npcSpriteRenderer.sprite = angrySprite;
                _chatBubble.UpdateChat(ChatBubble.IconType.Angry);
            }
            else if (_rewardPoints == 1)
            {
                _chatBubble.UpdateChat(ChatBubble.IconType.Annoyed);
            }
            else if (_rewardPoints == 2)
            {
                _chatBubble.UpdateChat(ChatBubble.IconType.Neutral);
            }
        }

        GameStats.instance.RunCheckpoint();
    }

    private void SetResult(bool questComplete, int points)
    {
        if (questComplete)
        {
            //inGameUI.GetComponent<InGameUI>().Points = points;
            GameStats.instance.points += points;
            //_gameUI.UpdateCustomers();
            _gameUI.UpdatePoints();
            
            if(GameStats.instance.points > 50 && GameStats.instance.AllUnlocked())
            {
               // GameStats.instance.ShowESOTY();
            }
        }

        gameObject.GetComponent<NPCBehavior>().WalkAway();
    }
    
}
