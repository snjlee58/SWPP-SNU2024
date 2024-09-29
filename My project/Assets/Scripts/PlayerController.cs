using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed = 20.0f;
    public float turnSpeed = 45.0f;

     // Get keyboard input
    public float horizontalInput;
    public float forwardInput;

    // Wheel Colliders
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Moves the car forward based on vertical input
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        
        // Rotates the car based on horizontal input
        // Vector3.up: rotation axis
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * horizontalInput);
    
        frontLeftWheelCollider.motorTorque = 10f * forwardInput;
        frontRightWheelCollider.motorTorque = 10f * forwardInput;
    }

    private void OnMouseDown() {
        RestartGame();
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
