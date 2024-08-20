using Characters.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// Represents an enemy that can deal damage to any GameObject with PlayerLife
public class Enemy : MonoBehaviour
{
    [SerializeField] private AudioSource DeathSound;
    [SerializeField] float mass;
    [SerializeField] private float massGiven;
    [SerializeField] private float minRelX;
    [SerializeField] private float maxRelX;
    [SerializeField] private float speed;
    private float originalX;
    private Boolean goingRight = true;
    private float originalScale;
    private float timeSinceChange = 0;
    private Boolean grabbed = false;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody.mass = mass;

        originalScale = transform.localScale.x;
        originalX = transform.position.x;
        rigidBody.velocity = Vector3.right * speed;
    }


    // Function for dealing damage to playerlife script. Use this for the tongue weapon
    private void OnCollisionEnter2D(Collision2D collision) //Paste if this object can deal damage to something else
    {
        if (collision.gameObject.tag == "Player")
        {
            //if (HitSound == null) Debug.LogError("HitSound is null on " + gameObject.name);
            if (player.GetComponent<Rigidbody2D>().mass < mass)
            {
                player.GetComponent<PlayerLife>().TakeDamage(1);
            }
            else
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

    private void Update()
    {
        if (!grabbed)
        {
            timeSinceChange += Time.deltaTime;
            if (timeSinceChange > 0.5)
            {
                if (transform.position.x >= originalX + maxRelX || transform.position.x <= originalX - minRelX)
                {
                    goingRight = !goingRight;
                    timeSinceChange = 0;
                }
            }


            if (goingRight)
            {
                rigidBody.velocity = new Vector3(speed, 0, 0);
                transform.localScale = new Vector2(originalScale * -1, transform.localScale.y);
            }
            else
            {
                rigidBody.velocity = new Vector3(-speed, 0, 0);
                transform.localScale = new Vector2(originalScale, transform.localScale.y);
            }
        }
    }

    private void OnDrawGizmos()
    {
        float spriteWidthHalf = GetComponent<SpriteRenderer>().bounds.size.x / 2f;
        Gizmos.DrawLine(transform.position - new Vector3(minRelX + spriteWidthHalf, 0, 0), 
            transform.position + new Vector3(maxRelX + spriteWidthHalf, 0, 0));
    }

    public void setGrab(Boolean grab)
    {
        grabbed = grab;
    }
}
