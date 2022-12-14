using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public static InGameUI instance;
    public GameObject pointTextObject;
    public GameObject customerCountTextObject;
    public GameObject customerIcon;
    private TextMeshProUGUI _pointText;
    private TextMeshProUGUI _customerCountText;

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

    private void Start()
    {

        _pointText = pointTextObject.GetComponent<TextMeshProUGUI>();
        _customerCountText = customerCountTextObject.GetComponent<TextMeshProUGUI>();
        UpdatePoints();
        GameStats.instance.RunStreakCheckpoint();
    }

    public void UpdatePoints()
    {
        //_pointText.SetText("Poäng: " + GameStats.instance.points); //Swedish
        _pointText.SetText("Points: " + GameStats.instance.points); //English
    }

    public void UpdateStreak()
    {
        _customerCountText.SetText("Streak: " + GameStats.instance.streakPoints);
    }
}
