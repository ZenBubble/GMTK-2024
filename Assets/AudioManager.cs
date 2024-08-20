using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource RunSFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip chameleonJump;
    public AudioClip chameleonEat; 
    public AudioClip chameleonRun;
    public AudioClip potionCollect;
    public AudioClip chameleonDamage;
    public AudioClip chameleonSlurp;
    public AudioClip chameleonSwing;
    public AudioClip chameleonDeath;
    public AudioClip mainMusic;
    public AudioClip desertMusic;
    public AudioClip pressurePlate;
    public AudioClip lever; 

    [HideInInspector]
    private Boolean isRunning = false;

    private void Start()
    {
        musicSource.clip = mainMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayRunSFX()
    {
        if (!isRunning)
        {
            isRunning = true;
            RunSFXSource.clip = chameleonRun;
            RunSFXSource.Play();
        }
        
    }
    public void stopRunSFX()
    {
        RunSFXSource.Stop();
        isRunning = false;
    }
}
