using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Represents the health system of the player. Ensure that the object has all of the fields
public class PlayerLife : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] private Image[] hearts;
    //public float maxHealth = 100;
    //public float currentHealth;
    //public float timer;
    //public float deathScreenTime = 100;

    private RestartScript restartScript;
    [SerializeField] float maxX = 200;
    [SerializeField] float maxY = 200;
    [SerializeField] float minX = 200;
    [SerializeField] float minY = -200;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        restartScript = GetComponent<RestartScript>();
        //currentHealth = maxHealth;
        // screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

     void Update()
    {
        if (transform.position.x < minX || transform.position.x > maxX ||
            transform.position.y < minY || transform.position.y > maxY)
        {
            audioManager.PlaySFX(audioManager.chameleonDamage);
            playerDie();
        }
    }

    // called by other functions to deal damage. for now sets isDead to true no matter what
    public void TakeDamage(float damageAmount) 
    {
        playerDie();
    }

    private void playerDie()
    {
        restartScript.RestartScene();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3((minX + maxX) / 2f, (minY + maxY) / 2f, 0), new Vector3(maxX - minX, maxY - minY, 0));
    }

    // // updates the visual hearts in hearts
    // private void UpdateHealth()
    // {
    //         for (int i = 0; i < hearts.Length; i++)
    //         {
    //             if (i < currentHealth)
    //             {
    //                 hearts[i].color = Color.white;
    //             }

    //             else
    //             {
    //                 hearts[i].color = Color.black;
    //             }
    //         }
    // }
}

