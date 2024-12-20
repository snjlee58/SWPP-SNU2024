using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // GameController
    public GameController gameController;

    // Player
    private Rigidbody playerRb;
    private Animator playerAnimator;

    // Collision
    private bool isGround = true;

    // Jump action
    private float jumpForce = 800.0f;

    // Move player
    private float moveSpeed = 8.0f;
    private float horizontalInput;

    // Particle
    public ParticleSystem coinParticle;

    // Audio
    private AudioSource playerAudio;
    public AudioClip jumpAudio;
    public AudioClip coinAudio;

    // Projectile object (used for removing animals in stage 4)
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start() {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= 2; 

        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        // Get key input for horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movementDirection = new Vector3(horizontalInput, 0, 0);
        movementDirection.Normalize();

        if (isStanding())  {
            // Set idle animation when no movement
            playerAnimator.SetBool("Static_b", true);
            playerAnimator.SetFloat("Speed_f", 0.0f);
        } else {
            // Set movement animation (walking)
            playerAnimator.SetBool("Static_b", false);
            playerAnimator.SetFloat("Speed_f", 0.3f);
            
            // Move player based on horizontal input
            if (movementDirection != Vector3.zero) {
                transform.forward = movementDirection;
            }
            transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);
        }

        // Jump action
        if (Input.GetKeyDown(KeyCode.Space) && isGround) {
            // Physics
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGround = false;

            // Trigger jump animation
            if (isStanding()) {
                // Standing jump
                playerAnimator.SetBool("Jump_b", true);
            } else {
                // Moving jump
                playerAnimator.SetTrigger("Jump_trig");
            }

            // Sound effect
            playerAudio.PlayOneShot(jumpAudio);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            // Launch a weapon (rock)
            Instantiate(projectilePrefab, transform.position + new Vector3(1, 1, 0), transform.rotation);
        }
    }

    private void OnCollisionEnter(Collision collision) {
            // Stop jump animation
            playerAnimator.SetBool("Jump_b", false);

        // Prevent double jump
        if (collision.gameObject.CompareTag("Ground")) {
            isGround = true;
        }

        if (collision.gameObject.CompareTag("Money")) {
            // Check level up 
            gameController.CoinCollected();
            
            // Effects
            coinParticle.Play();
            playerAudio.PlayOneShot(coinAudio);

            // Destroy coin on collision
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Enemy")) {
            Vector3 playerPosition = transform.position;
            Vector3 enemyPosition = collision.gameObject.transform.position;

            // Destroy enemy when player jumps on top
            if (playerPosition.y > enemyPosition.y + 1.0f) 
            {
                Destroy(collision.gameObject);
            } else {
                Die();
            }
        }
    }

    private bool isStanding() {
        // Check whether player is standing
        return horizontalInput == 0.0f;
    }

    public void Die() {
        playerAnimator.SetBool("Death_b", true);
        playerAnimator.SetInteger("DeathType_int", 2);
    }
}
