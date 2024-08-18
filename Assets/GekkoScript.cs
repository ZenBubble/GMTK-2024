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
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(horizontalInput * speed, rigidBody.velocity.y);

        // sets animation to idle
        animator.SetFloat("xVelocity", 0);

        // flip player sprite depending on movement direction an updates animation
        if (horizontalInput > 0.01f)
        {
            transform.localScale = originalScale;
            animator.SetFloat("xVelocity", 1);
        } else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(originalScale.x * -1, originalScale.y, originalScale.z);
            animator.SetFloat("xVelocity", 1);
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
