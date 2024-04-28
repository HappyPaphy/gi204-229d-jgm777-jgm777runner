using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource clickingUISound;
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource loseSound;
    public static SoundManager instance;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    public void UIClickSound()
    {
        clickingUISound.Play();
    }

    public void WinSound()
    {
        winSound.Play();
    }

    public void LoseSound()
    {
        loseSound.Play();
    }
}
