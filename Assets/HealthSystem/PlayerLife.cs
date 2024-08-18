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
    [SerializeField] private Image[] hearts;
    private Rigidbody2D rb;
    private Vector3 screenBounds;
    public Boolean isDead;
    public GameObject player;
    public float maxHealth = 100;
    public float currentHealth;
    public float timer;
    public float deathScreenTime = 100;

    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
        rb = GetComponent<Rigidbody2D>();
        // screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // checks if out of bounds TODO: implement
    // void Update()
    // {
    //     if(transform.position.x < screenBounds.x * -4 || transform.position.x > screenBounds.x * 4){
    //         //RestartLevel();
    //     }
    //     else if(transform.position.y < screenBounds.y * -4)
    //     {
    //         //RestartLevel();
    //     }        
    // }

    // called by other functions to deal damage. for now only sets isDead to true when health is 0.
    public void TakeDamage(float damageAmount) 
    {
        currentHealth -= damageAmount;
        if (currentHealth > 0)
        {
            UpdateHealth();
        }
        if (currentHealth == 0)
        {
            UpdateHealth();
            isDead = true;
        }
    }

    // updates the visual hearts in hearts
    private void UpdateHealth()
    {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < currentHealth)
                {
                    hearts[i].color = Color.white;
                }

                else
                {
                    hearts[i].color = Color.black;
                }
            }
    }
}

