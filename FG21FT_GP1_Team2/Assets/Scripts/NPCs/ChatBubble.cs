using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour
{
    public enum IconType
    {
        Happy,
        Neutral,
        Annoyed,
        Angry,
    }

    [SerializeField] private Sprite _happyIconSprite;
    [SerializeField] private Sprite _neutralIconSprite;
    [SerializeField] private Sprite _annoyedIconSprite;
    [SerializeField] private Sprite _angryIconSprite;
    [SerializeField] private List<string> _lines = new List<string>();

    private string _startOfSentence;
    private SpriteRenderer _iconSprite;
    private TextMeshPro _questText;

    public void SetupChat(IconType iconType, string potionDescription)
    {
        _startOfSentence = _lines[Random.Range(0, _lines.Count)];
        _questText = transform.GetChild(0).Find("Text").GetComponent<TextMeshPro>();
        _iconSprite = transform.GetChild(0).Find("EmoteIcon").GetComponent<SpriteRenderer>();

        _questText.SetText(_startOfSentence + " " + "<color=#FF0000>" + potionDescription + ". </color>");

        _iconSprite.sprite = GetIconSprite(iconType);
    }

    public void UpdateChat(IconType iconType)
    {
        _iconSprite = transform.GetChild(0).Find("EmoteIcon").GetComponent<SpriteRenderer>();
        _iconSprite.sprite = GetIconSprite(iconType);
    }

    public void updateIcon(IconType iconType)
    {
        _iconSprite.sprite = GetIconSprite(iconType);
    }

    private Sprite GetIconSprite(IconType iconType)
    {
        switch (iconType)
        {
            default:
            case IconType.Happy: return _happyIconSprite;
            case IconType.Neutral: return _neutralIconSprite;
            case IconType.Annoyed: return _annoyedIconSprite;
            case IconType.Angry: return _angryIconSprite;
        }
    }
}
