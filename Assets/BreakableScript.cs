using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 normal = collision.contacts[0].normal;
            float mass = collision.gameObject.GetComponent<Rigidbody2D>().mass;
            Vector2 velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
        }
    }
}
