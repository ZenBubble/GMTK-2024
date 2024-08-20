using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to open a door when a player or object exceeding a certain mass enters the trigger
public class DoorTrigger : MonoBehaviour
{
    // Door object to be moved
    [SerializeField] private GameObject door;
    // Boolean to check if the door is open
    private bool isOpen = false;

    // Mass threshold for the door to open
    public float massThreshold = 10.0f;

    [SerializeField] private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When the player or object exceeding a certain mass enters the trigger, the door moves up
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOpen)
        {
            Rigidbody2D rb = other.attachedRigidbody;

            if (rb != null && rb.mass >= massThreshold)
            {
                door.transform.position += new Vector3(0, 5, 0);
                isOpen = true;
            }
            audioManager.PlaySFX(audioManager.pressurePlate);


        }
        
    }

    // When the player or object exits the trigger, the door moves down
    private void OnTriggerExit2D(Collider2D other)
    {
        if (isOpen)
        {
            door.transform.position += new Vector3(0, -5, 0);
            isOpen = false;
        }
    }
}
