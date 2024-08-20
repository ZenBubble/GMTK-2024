using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Implements fragile platforms, break if player mass > maxMass
/// </summary>
public class FragilePlatform : MonoBehaviour
{
    [SerializeField] private float maxMass;
    [SerializeField] private ContactFilter2D contactFilter;
    [SerializeField] private TextMeshProUGUI weightDisplay;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite brokenSprite;
    private BoxCollider2D boxCollider;
    private GameObject playerObject;
    private Rigidbody2D playerRigidBody;
    

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        GetComponent<SpriteRenderer>().sprite = normalSprite;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerRigidBody = playerObject.GetComponent<Rigidbody2D>();
        if (weightDisplay != null) {
            weightDisplay.text = maxMass.ToString();
        }
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
        GetComponent<SpriteRenderer>().sprite = brokenSprite;
        GetComponent<BoxCollider2D>().enabled = false;
        //GetComponent<SpriteRenderer>().enabled = false;
    }
}
