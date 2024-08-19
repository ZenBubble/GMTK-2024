using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.deltaTime;
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody.mass = mass;
    }


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
            else if (PlayerComponent.rb.mass >= mass)
            {
                    PlayerComponent.GetComponent<Rigidbody2D>().mass = PlayerComponent.GetComponent<Rigidbody2D>().mass + massGiven;
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
