using UnityEngine;

public class LevelLoseSound : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip levelLose;

    private void Start()
    {
        musicSource.clip = levelLose;
        musicSource.Play();
    }

    
}
