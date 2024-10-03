using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnimator;

    // Collision
    private bool isGround = true;

    // Jump action
    private float jumpForce = 800.0f;

    // Move player
    private float moveSpeed = 8.0f;
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= 2; 

        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get L/R key input
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput == 0.0f)  {
            // Set idle animation when no movement
            playerAnimator.SetBool("Static_b", true);
            playerAnimator.SetFloat("Speed_f", 0.0f);
        } else {
            // Set movement animation (walking)
            playerAnimator.SetBool("Static_b", false);
            playerAnimator.SetFloat("Speed_f", 0.3f);
        }

        // Move player based on horizontal input
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * horizontalInput);

        // Jump action
        if (Input.GetKeyDown(KeyCode.Space) && isGround) {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGround = false;

            // Trigger jump animation
            playerAnimator.SetTrigger("Jump_trig");
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Stop jump animation
        playerAnimator.SetBool("Jump_b", false);

        // Prevent double jump
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}
