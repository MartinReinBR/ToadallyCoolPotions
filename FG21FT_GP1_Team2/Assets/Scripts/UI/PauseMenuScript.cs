using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    //Menu
    public GameObject menuUI;
    public bool menuOn;
    public GameObject SettingsPanel;

    // Start is called before the first frame update
    void Start()
    {
        menuOn = false;
    }

    // Update is called once per frame
    void Update()
    {


        menuUI.SetActive(menuOn);
        if (Input.GetKeyDown(KeyCode.Escape) && !menuOn)
        {
            PauseGame();

        }
        else
        if (Input.GetKeyDown(KeyCode.Escape) && menuOn)
        {
            ResumeGame();

        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        menuOn = true;
        //Cursor.visible = true;
        Debug.Log("Escape key was pressed");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        //Cursor.visible = false;
        SettingsPanel.SetActive(false);
        menuOn = false;
        Debug.Log("Escape key was pressed again");
    }
}
