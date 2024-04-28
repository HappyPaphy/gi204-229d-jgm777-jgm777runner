using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] public SoundManager soundManager;

    public static SceneChanger instance;
    

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    public void RestartScene()
    {
        soundManager.UIClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeScene(string sceneName)
    {
        soundManager.UIClickSound();
        SceneManager.LoadScene(sceneName);
    }// MainMenuScene, PlayScene

    public void ExitGame()
    {
        Application.Quit();
    }

    

    

    

    


    
}
