using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    [SerializeField] private Button button_MainMenu;
    [SerializeField] private Button button_PlayGame;

    [Header("Credit")]
    [SerializeField] private Button button_CreditUI;
    [SerializeField] private Button button_BackCreditUI;
    [SerializeField] private GameObject creditUI;

    [Header("Tutorial")]
    [SerializeField] private Button button_TutorialUI;
    [SerializeField] private Button button_BackTutorialUI;
    [SerializeField] private GameObject blackBlurBackGround;
    [SerializeField] private GameObject[] tutorialUIArray;
    [SerializeField] private int tutorialUIIndex;


    [Header("Quit")]
    [SerializeField] private Button button_QuitUI;
    [SerializeField] private Button button_ConfirmQuitUI;
    [SerializeField] private Button button_CancelQuitUI;
    [SerializeField] private GameObject quitUI;

    [Header("Setting")]
    [SerializeField] private Button button_SettingUI;
    [SerializeField] private Button button_BackSettingUI;
    [SerializeField] private GameObject settingUI;

    [Header("Script")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SoundManager soundManager;

    void Start()
    {
        if (tutorialUIArray != null)
        {
            CloseAllTutorialUI();
        }

        if (creditUI != null)
        {
            creditUI.SetActive(false);
        }

        if (settingUI != null)
        {
            settingUI.SetActive(false);
        }

        if (quitUI != null)
        {
            quitUI.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    public void QuitGameScene()
    {
        uiManager.OpenUI(creditUI, true);
        soundManager.UIClickSound();
        Debug.Log("Quit Game");
    }

    public void PlayGame()
    {
        soundManager.UIClickSound();
        SceneChanger.instance.ChangeScene("PlayScene");
    }

    public void ExitGame()
    {
        SceneChanger.instance.ExitGame();
        soundManager.UIClickSound();
        Debug.Log("Quit Game");
    }

    public void OpenCreditUI()
    {
        uiManager.OpenUI(creditUI, true);
    }

    public void CloseCreditUI()
    {
        uiManager.OpenUI(creditUI, false);
    }

    public void OpenTutorialUI(int i)
    {
        CloseAllTutorialUI();
        uiManager.OpenUI(tutorialUIArray[i], true);
        blackBlurBackGround.SetActive(true);
    }

    public void CloseAllTutorialUI()
    {
        blackBlurBackGround.SetActive(false);
        foreach (GameObject tutorialUI in tutorialUIArray)
        {
            tutorialUI.SetActive(false);
        }
    }

    public void OpenSettingUI()
    {
        uiManager.OpenUI(settingUI, true);
    }

    public void CloseSettingUI()
    {
        uiManager.OpenUI(settingUI, false);
    }

    public void OpenQuitUI()
    {
        uiManager.OpenUI(quitUI, true);
    }

    public void CloseQuitUI()
    {
        uiManager.OpenUI(quitUI, false);
    }
}
