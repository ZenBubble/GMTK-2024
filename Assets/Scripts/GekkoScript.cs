using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GekkoScript : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask groundLayer;
    private Vector3 originalScale;
    [SerializeField] private ContactFilter2D contactFilter;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(horizontalInput * speed, rigidBody.velocity.y);

        // flip player sprite depending on movement direction
        if (horizontalInput > 0.01f)
        {
            transform.localScale = originalScale;
        } else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(originalScale.x * -1, originalScale.y, originalScale.z);
        }

        // vertical movement
        if (Input.GetKey(KeyCode.Space))
        {
            jump();
        }
    }

    // jumps if the player is grounded
    public void jump()
    {
        if (isGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpHeight);
        }
    }


    // checks if player is grounded by sending a raycast downwards
    private Boolean isGrounded()
    {
        return rigidBody.IsTouching(contactFilter);
    }
}
