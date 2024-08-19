using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rotates scale arrow based on given player's mass
public class rotateBasedOnMass : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Rigidbody2D rb;
    private float mass;
    private float maxWeight = 100;
    private float rotation;
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mass = rb.mass;
        rotation = mass/maxWeight * -360;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
