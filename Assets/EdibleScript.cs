using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using UnityEngine;

public class EdibleScript : MonoBehaviour
{
    [SerializeField] private float massGiven;
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private GekkoScript gekkoScript;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gekkoScript = player.GetComponent<GekkoScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Rigidbody2D>().mass = player.GetComponent<Rigidbody2D>().mass + massGiven;
            destroyObject();
        }
    }

    private void destroyObject()
    {
        spriteRenderer.enabled = false;
        circleCollider.enabled = false;
    }
}
