using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    // Collision
    private bool isGround = true;

    // Jump action
    private float jumpForce = 800.0f;

    // Move player
    private float moveSpeed = 8.0f;
    private float forwardInput;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= 2; 
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Horizontal");

        // Move player based on horizontal input
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * forwardInput);

        // Jump action
        if (Input.GetKeyDown(KeyCode.Space) && isGround) {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGround = false;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Prevent double jump
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}
