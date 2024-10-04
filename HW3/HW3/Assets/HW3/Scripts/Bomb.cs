using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public ParticleSystem bombParticle;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update() {
    }

    // Method to handle the bomb explosion
    public void Explode() {
        // Explosion particle effect
        ParticleSystem explosion = Instantiate(bombParticle, transform.position, transform.rotation);
        explosion.Play();
        Destroy(explosion.gameObject, explosion.main.duration + explosion.main.startLifetime.constantMax);

        // Check for  player within the explosion radius
        CheckForNearbyPlayer();

        // Destroy the bomb after the explosion
        Destroy(gameObject);
    }

    // Check for player within the explosion radius
    private void CheckForNearbyPlayer() {
        // Create a sphere around the bomb's position with the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);

        foreach (Collider hitCollider in hitColliders) {
            if (hitCollider.CompareTag("Player")) {
                // Player death animation
                Animator playerAnimator = hitCollider.gameObject.GetComponent<Animator>();
                if (playerAnimator != null) {
                    playerAnimator.SetBool("Death_b", true);
                    playerAnimator.SetInteger("DeathType_int", 2);
                }
            }
        }
    }
}
