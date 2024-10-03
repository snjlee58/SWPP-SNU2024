using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public ParticleSystem bombParticle;  // Prefab for the particle effect
    public GameObject bombPrefab;         // Prefab for the bomb itself
    public float explosionDelay = 10.0f;   // Delay before the bomb explodes
    public float explosionRadius = 1.0f;  // Radius in which the player will be affected
    public float respawnDelay = 3.0f;     // Delay before the bomb respawns
    public GameObject player;             // Reference to the player object
    private Animator playerAnimator;

    private Vector3 originalPosition;     // Store the original position of the bomb

    // Start is called before the first frame update
    void Start()
    {
        // Store the original position of the bomb
        originalPosition = transform.position;

         // Get the Animator component from the player
        playerAnimator = player.GetComponent<Animator>();

        // Invoke the Explosion method after 'explosionDelay' seconds, and repeat every 'explosionDelay' seconds
        InvokeRepeating("Explode", explosionDelay, explosionDelay);
    }

    // Method to handle the bomb explosion
    void Explode()
    {
        // Instantiate the explosion particle effect
        // Instantiate(explosionParticle, transform.position, Quaternion.identity);
        bombParticle.Play();

        // Check if the player is within the explosion radius
        if (Vector3.Distance(transform.position, player.transform.position) <= explosionRadius)
        {
            // Kill the player (you can trigger death animation or destroy the player object)
            // Destroy(player);  // Simple example of destroying the player
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 2);
            Debug.Log("Player has been killed by the explosion!");
        }

        // Destroy the bomb after the explosion
        Destroy(gameObject);  // Remove the bomb object

        // Respawn the bomb after a delay
        Invoke("RespawnBomb", respawnDelay);
    }

    // Method to respawn the bomb
    void RespawnBomb()
    {
        // Instantiate a new bomb at the original position
        Instantiate(bombPrefab, originalPosition, Quaternion.identity);
    }
}
