using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopupMessageManager : MonoBehaviour
{
    public static PopupMessageManager instance;
    private List<PopupMessage> popupMessages = new List<PopupMessage>();
    private RectTransform popupRectTransform;
    private bool isPlaying = false;

    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text header;
    [SerializeField] private TMP_Text footer;
    [SerializeField] private GameObject skipTutorial;

    [SerializeField] private AnimationCurve inCurve;
    [SerializeField] private AnimationCurve outCurve;
    private AnimationCurve curve;
    private float defaultDuration = 2f;
    private float customDuration;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this.gameObject);

        popupRectTransform = GetComponent<RectTransform>();
        skipTutorial.SetActive(false);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("TutorialComplete") == 1) { RequestDisplayPopupMessage(new PopupMessage(null, "Press to skip tutorial"), 5f); }
    }

    public void RequestDisplayPopupMessage(PopupMessage message)
    {
        popupMessages.Add(message);
        customDuration = defaultDuration;
        if (!isPlaying) SetupDisplayPopupMessage();
    }
    public void RequestDisplayPopupMessage(PopupMessage message, float duration)
    {
        popupMessages.Add(message);
        customDuration = duration;
        skipTutorial.SetActive(true);
        if (!isPlaying) SetupDisplayPopupMessage();
    }
    public void RequestDisplayPopupMessage(Sprite s, string h, string f) => RequestDisplayPopupMessage(new PopupMessage(s, h, f));

    private void SetupDisplayPopupMessage()
    {
        DisplayPopupMessage(popupMessages[0]);
        popupMessages.RemoveAt(0);
    }

    private void DisplayPopupMessage(PopupMessage message)
    {
        if (message.icon == null && message.header == string.Empty && message.footer == string.Empty) return;

        icon.gameObject.SetActive(true);
        if (message.icon == null) icon.gameObject.SetActive(false);
        else icon.sprite = message.icon;

        header.gameObject.SetActive(true);
        if (message.header == null) header.gameObject.SetActive(false);
        else header.text = message.header.ToUpper();

        footer.gameObject.SetActive(true);
        if (message.footer == null) footer.gameObject.SetActive(false);
        else footer.text = message.footer;

        StartCoroutine(DisplayingMessage());
    }

    public void SkipTutorial()
    {
        GameStats.instance.tutorialStage = 3;
        SpawnBaseIngredient.instance.SpawnExtraIngredients();
        EndDisplayCurrentMessage();
    }

    public void EndDisplayCurrentMessage()
    {
        StopAllCoroutines();
        StartCoroutine(EndDisplayingMessage());
    }

    private IEnumerator DisplayingMessage()
    {
        float t = 0f;
        isPlaying = true;

        curve = inCurve;

        while (t < 1f)
        {
            t += Time.deltaTime;
            UpdatePopupMessagePosition(t);
            yield return null;
        }

        yield return new WaitForSeconds(customDuration);

        curve = outCurve;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            UpdatePopupMessagePosition(t);
            yield return null;
        }

        isPlaying = false;
        if (popupMessages.Count > 0) SetupDisplayPopupMessage();
        skipTutorial.SetActive(false);
    }
    private IEnumerator EndDisplayingMessage()
    {
        float t = 1f;
        curve = outCurve;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            UpdatePopupMessagePosition(t);
            yield return null;
        }

        isPlaying = false;
        if (popupMessages.Count > 0) SetupDisplayPopupMessage();
        skipTutorial.SetActive(false);
    }
    private void UpdatePopupMessagePosition(float progress) => popupRectTransform.anchoredPosition = Vector3.LerpUnclamped(new Vector3(0f, 120f, 0f), new Vector3(-270f, 120f, 0f), curve.Evaluate(progress));
}
