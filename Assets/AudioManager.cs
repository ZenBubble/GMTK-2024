using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

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

    private void Start()
    {
        musicSource.clip = mainMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
