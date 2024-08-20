using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using UnityEngine;

// Represents an enemy that can deal damage to any GameObject with PlayerLife
public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioSource DeathSound;
    [SerializeField] float mass;
    [SerializeField] private float massGiven;
    private Rigidbody2D rigidBody;
    public float secondsPerAttack = 2;
    public float damageToPlayer = 1;
    public float timer;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.deltaTime;
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody.mass = mass;
    }


    // Function for dealing damage to playerlife script. Use this for the tongue weapon
    private void OnCollisionEnter2D(Collision2D collision) //Paste if this object can deal damage to something else
    {
        if (collision.gameObject.tag == "Player")
        {
            //if (HitSound == null) Debug.LogError("HitSound is null on " + gameObject.name);
            if (timer > secondsPerAttack && player.GetComponent<Rigidbody2D>().mass < mass)
            {
                player.GetComponent<PlayerLife>().TakeDamage(damageToPlayer);
                timer = 0;
            }
            else if (player.GetComponent<Rigidbody2D>().mass >= mass)
            {
                    player.GetComponent<GekkoScript>().eat(massGiven);
                    destroyObject();
            }
        }

    }

    private void destroyObject()
    {
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
    }
}
