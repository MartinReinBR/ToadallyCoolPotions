using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public string startGameScene;
    public string mainMenuScene;
    public GameObject SettingPanel;
    private GameObject PausePanel;

    private void Start()
    {
        PausePanel = transform.parent.gameObject;
        if(SettingPanel != null) SettingPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void OpenSettingsPAnel()
    {
        SettingPanel.SetActive(true);
        PausePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
