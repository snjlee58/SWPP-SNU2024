using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    // Jump action
    private float jumpForce = 600.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= 1; 
    }

    // Update is called once per frame
    void Update()
    {
        // Jump action
        if (Input.GetKeyDown(KeyCode.Space)) {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
    }
}
