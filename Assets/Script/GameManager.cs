using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject youWinUI;
    [SerializeField] private GameObject youLoseUI;

    [SerializeField] public SoundManager soundManager;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (youWinUI != null)
        {
            youWinUI.SetActive(false);
        }

        if (youLoseUI != null)
        {
            youLoseUI.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    public void YouWinUI()
    {
        soundManager.WinSound();
        youWinUI.SetActive(true);
    }

    public void YouLoseUI()
    {
        soundManager.LoseSound();
        youLoseUI.SetActive(true);
    }
}
