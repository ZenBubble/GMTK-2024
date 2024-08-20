using UnityEngine;

public class LevelStartSound : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip levelStart;

    private void Start()
    {
        musicSource.clip = levelStart;
        musicSource.Play();
    }

}
