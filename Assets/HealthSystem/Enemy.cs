using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents an enemy that can deal damage to any GameObject with PlayerLife
public class Enemy : MonoBehaviour
{
    [SerializeField] float currentHealth, maxHealth = 3f;
    [SerializeField] private AudioSource DeathSound;
    [SerializeField] float mass;
    private Rigidbody2D rigidBody;
    public float secondsPerAttack = 2;    
    public float damageToPlayer = 1;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        timer = Time.deltaTime;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.mass = mass;
    }

    // Allows other objects to deal damage to this object. Not currently implemented since tongue weapon isn't implemented yet
    // public void TakeDamage(float damageAmount) 
    // {
    //     if (gibsLocation != null)
    //     {
    //         gibsLocation.GetComponent<ParticleSystem>().Play();
    //     }
    //     else
    //     {
    //         GetComponent<ParticleSystem>().Play();
    //     }
    //     currentHealth -= damageAmount;
    //     if (currentHealth <= 0)
    //     {
    //         DeathSound.Play();
    //         DestroyFunction();
    //     }
    // }

    // Function for dealing damage to playerlife script. Use this for the tongue weapon
    private void OnCollisionStay2D(Collision2D collision) //Paste if this object can deal damage to something else
    {
        if (collision.gameObject.TryGetComponent<PlayerLife>(out PlayerLife PlayerComponent))
        {
            //if (HitSound == null) Debug.LogError("HitSound is null on " + gameObject.name);
            if (timer > secondsPerAttack && PlayerComponent.rb.mass < mass)
            {
                PlayerComponent.TakeDamage(damageToPlayer);
                timer = 0;
            }

        }

    }

    void Update()
    {
        timer += Time.deltaTime;
    }
}
