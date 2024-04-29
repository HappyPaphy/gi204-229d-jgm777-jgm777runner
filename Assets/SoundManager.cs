using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource clickingUISound;
    [SerializeField] private AudioSource collectItemSound;
    [SerializeField] private AudioSource craftItemSound;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource playerHurtSound;
    [SerializeField] private AudioSource playerRunSound;
    [SerializeField] private AudioSource cannonShootSound;
    [SerializeField] private AudioSource playerDiedSound;
    [SerializeField] private AudioSource waterSound;

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

    public void PlayerHurtSound()
    {
        playerHurtSound.Play();
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

    public void CollectItemSound()
    {
        collectItemSound.Play();
    }

    public void CraftItemSound()
    {
        craftItemSound.Play();
    }

    public void JumpSound()
    {
        jumpSound.Play();
    }

    public void PlayerRunSound()
    {
        playerRunSound.Play();
    }

    public void CannonShootSound()
    {
        cannonShootSound.Play();
    }

    public void PlayerDiedSound()
    {
        playerDiedSound.Play();
    }

    public void WaterSound()
    {
        waterSound.Play();
    }
}
