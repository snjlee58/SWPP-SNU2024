using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // public ParticleSystem bombParticle;  // Prefab for the particle effect
    public GameObject bombParticle;  // Drag your Particle System prefab here

    // public GameObject player;             // Reference to the player object

    // public float explosionDelay = 5.0f;   // Delay before the bomb explodes
    // public float spawnInterval = 3.0f;   // Delay before the bomb explodes
    // // public float explosionRadius = 1.0f;  // Radius in which the player will be affected
    // public float respawnInterval = 3.0f;     // Delay before the bomb respawns
    // private Animator playerAnimator;

    // private Vector3 originalPosition;     // Store the original position of the bomb
    // private Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        // Store the original position of the bomb
        // originalPosition = transform.position;
        // originalRotation = transform.rotation;

         // Get the Animator component from the player
        // playerAnimator = player.GetComponent<Animator>();

        // Invoke the Explosion method after 'explosionDelay' seconds, and repeat every 'spawnInterval' seconds
        // InvokeRepeating("Explode", explosionDelay, spawnInterval);
    }

    void Update() {

    }

    // Method to handle the bomb explosion
    public void Explode()
    {
        // Instantiate the explosion particle effect
        // Instantiate(explosionParticle, transform.position, Quaternion.identity);
        // bombParticle.Play();
        Instantiate(bombParticle, transform.position, Quaternion.identity);
        GetComponent<ParticleSystem>().Play();

        // Check if the player is within the explosion radius
        // if (Vector3.Distance(transform.position, player.transform.position) <= explosionRadius)
        // {
        //     // Kill the player (you can trigger death animation or destroy the player object)
        //     // Destroy(player);  // Simple example of destroying the player
        //     playerAnimator.SetBool("Death_b", true);
        //     playerAnimator.SetInteger("DeathType_int", 2);
        //     Debug.Log("Player has been killed by the explosion!");
        // }

        // Destroy the bomb after the explosion
        Destroy(gameObject);  // Remove the bomb object

        // Respawn the bomb after a delay
        // Invoke("RespawnBomb", respawnInterval);
    }

    // Method to respawn the bomb
    // void RespawnBomb()
    // {
    //     Debug.Log(gameObject);
    //     // Instantiate a new bomb at the original position
    //     Instantiate(gameObject, originalPosition, originalRotation);
    // }
}
