using UnityEngine;
using UnityEngine.UI;

// Represents a script that updates the enemy icon based on whether its mass is greater than the player's.
public class EnemyIconScript : MonoBehaviour
{
    [SerializeField] private Image enemyIconImage; // Reference to the Image component
    [SerializeField] private Sprite enemyHeavierIcon;  // Icon when enemy is heavier or equal

    private Rigidbody2D playerRigidbody; // Reference to the player's Rigidbody2D
    private Rigidbody2D enemyRigidbody;  // Reference to the enemy's Rigidbody2D

    void Start()
    {
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        enemyRigidbody = GetComponent<Rigidbody2D>();

        UpdateIcon();
    }

    void Update()
    {
        // Update the enemy icon
        UpdateIcon();
    }

    // Updates the icon based on mass comparison
    private void UpdateIcon()
    {
        if (playerRigidbody.mass >= enemyRigidbody.mass)
        {
            enemyIconImage.enabled = false;  // Hide the image if the player is heavier
        }
        else
        {
            enemyIconImage.enabled = true;   // Show the image if the enemy is heavier or equal
            enemyIconImage.sprite = enemyHeavierIcon;
        }
    }
}
