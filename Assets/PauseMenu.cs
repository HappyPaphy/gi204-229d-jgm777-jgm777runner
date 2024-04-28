using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private Button button_Pause;
    [SerializeField] private Button button_Resume;
    [SerializeField] private Button button_Restart;
    [SerializeField] private GameObject pauseUI;

    [Header("Tutorial")]
    [SerializeField] private Button button_TutorialUI;
    [SerializeField] private Button button_BackTutorialUI;
    [SerializeField] private GameObject tutorialUI;

    [Header("Setting")]
    [SerializeField] private Button button_SettingUI;
    [SerializeField] private Button button_BackSettingUI;
    [SerializeField] private GameObject settingUI;

    [Header("Quit")]
    [SerializeField] private Button button_QuitUI;
    [SerializeField] private Button button_ConfirmQuitUI;
    [SerializeField] private Button button_CancelQuitUI;
    [SerializeField] private GameObject quitUI;

    [Header("Script")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SoundManager soundManager;

    void Start()
    {
        if (pauseUI != null)
        {
            pauseUI.SetActive(false);
        }

        if (tutorialUI != null)
        {
            tutorialUI.SetActive(false);
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

    public void Pause()
    {
        uiManager.OpenUI(pauseUI, true);
    }

    public void Resume()
    {
        uiManager.OpenUI(pauseUI, false);
    }

    public void Restart()
    {
        SceneChanger.instance.RestartScene();
    }

    public void OpenTutorialUI()
    {
        uiManager.OpenUI(tutorialUI, true);
    }

    public void CloseTutorialUI()
    {
        uiManager.OpenUI(tutorialUI, false);
    }

    public void OpenSettingUI()
    {
        uiManager.OpenUI(settingUI, true);
    }

    public void CloseSettingUI()
    {
        uiManager.OpenUI(settingUI, false);
    }
    public void BackToMainMenu()
    {
        SceneChanger.instance.ChangeScene("MainMenuScene");
        soundManager.UIClickSound();
        Debug.Log("Back To MainMenu");
    }
}
