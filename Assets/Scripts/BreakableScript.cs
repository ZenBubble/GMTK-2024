using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implements breakable walls/platforms
/// </summary>
public class BreakableScript : MonoBehaviour
{
    [SerializeField] float breakForce;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite brokenSprite;

    private void Start() {
        GetComponent<SpriteRenderer>().sprite = normalSprite;
    }

    /// <summary>
    /// On collision with player, calculate kinetic energy of collision and break object if force exceeds break force
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 normal = collision.GetContact(0).normal;
            float mass = collision.gameObject.GetComponent<Rigidbody2D>().mass;
            float velocity = collision.relativeVelocity.magnitude;

            float force = (float) (0.5 * mass * Math.Pow(velocity, 2));

            if (force > breakForce)
            {
                breakObject();
            }
        }
    }

    /// <summary>
    /// break the object
    /// </summary>
    private void breakObject()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = brokenSprite;
        //GetComponent<SpriteRenderer>().enabled = false;
    }
}
