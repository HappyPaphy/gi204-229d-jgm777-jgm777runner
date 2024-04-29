using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLoseUI : MonoBehaviour
{
    [SerializeField] private GameObject winLoseUIPanel;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject lostText;

    [SerializeField] private Button button_Replay;
    [SerializeField] private Button button_MainMenu;

    public static WinLoseUI instance;
    

    void Start()
    {
        instance = this;

        winLoseUIPanel.SetActive(false);
        winText.SetActive(false);
        lostText.SetActive(false);
    }

    public void YouWin()
    {
        winLoseUIPanel.SetActive(true);
        winText.SetActive(true);
        lostText.SetActive(false);
    }

    public void YouLost()
    {
        winLoseUIPanel.SetActive(true);
        winText.SetActive(false);
        lostText.SetActive(true);
    }

    public void ReplayTheScene()
    {
        SoundManager.instance.UIClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMainMenu()
    {
        SoundManager.instance.UIClickSound();
        SceneChanger.instance.ChangeScene("MainMenuScene");
    }
}
