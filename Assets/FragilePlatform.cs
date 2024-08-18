using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implements fragile platforms, break if player mass > maxMass
/// </summary>
public class FragilePlatform : MonoBehaviour
{
    [SerializeField] private float maxMass;
    [SerializeField] private ContactFilter2D contactFilter;
    private BoxCollider2D boxCollider;
    [SerializeField] private GameObject playerObject;
    private Rigidbody2D playerRigidBody;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        playerRigidBody = playerObject.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Check if platform is touching player. If player exceeds maxMass, break 
    /// </summary>
    private void FixedUpdate()
    {
        if (boxCollider.IsTouching(contactFilter))
        {
            if (playerRigidBody.mass > maxMass)
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
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
