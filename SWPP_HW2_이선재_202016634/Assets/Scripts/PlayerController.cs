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

    // Reference to front wheels
    public Transform frontLeftWheel;
    public Transform frontRightWheel;

    // Maximum angle the wheels can turn while steering
    public float maxSteerAngle = 10.0f;

    private Rigidbody carRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        // carRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from keyboard
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Moves the car forward based on vertical input
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        // Rotates the car based on horizontal input
        // Vector3.up: rotation axis
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * horizontalInput);
    
        // Steer wheels based on horizontal input
        SteerWheels(); 
    
    }

    void SteerWheels() {
        // Calculate steering angle
        float steerAngle = horizontalInput * maxSteerAngle;

        // Rotate front wheels
        frontLeftWheel.localRotation = Quaternion.Euler(0, steerAngle, 0);
        frontRightWheel.localRotation = Quaternion.Euler(0, steerAngle, 0);
    }

    void MoveCar()
    {
        // Calculate forward movement
        Vector3 forwardMovement = transform.forward * forwardInput * speed * Time.deltaTime;

        // Use Rigidbody to move the car with physics
        carRigidbody.MovePosition(carRigidbody.position + forwardMovement);
    }

    void RotateCar()
    {
        // Calculate the amount of rotation
        float turn = horizontalInput * turnSpeed * Time.deltaTime;

        // Use Rigidbody to rotate the car with physics
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        carRigidbody.MoveRotation(carRigidbody.rotation * turnRotation);
    }

    private void OnMouseDown()
    {
        RestartGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
