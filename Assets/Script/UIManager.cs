using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private SoundManager soundManager;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OpenUI(GameObject gameObject, bool b)
    {
        soundManager.UIClickSound();
        gameObject.SetActive(b);
    }
}
