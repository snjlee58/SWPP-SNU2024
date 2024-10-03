using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public WheelCollider[] wheel_col;
    public Transform[] wheels;
   
    float torque = 500f;
    float steerAngle = 45;

    private float forwardInput;
    private float horizontalInput;

    public Vector3 centerOfMassOffset = new Vector3(0, -0.6f, 0);  // Adjust Y value to lower the center of mass
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // rb.centerOfMass = rb.centerOfMass + centerOfMassOffset;
    }

    void Update()
    {
        for(int i=0;i<wheel_col.Length;i++)
        {
            forwardInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");

            if(i==0||i==1)
            {
                // Apply steer wheel rotation for front wheels only
                wheel_col[i].steerAngle = horizontalInput * steerAngle;
            }

            // Check if the player has released Vertical key input
            if (Mathf.Abs(forwardInput) < 0.1f) 
            {
                // Apply brake to gradually stop the car
                wheel_col[i].brakeTorque = 6000;  // Adjust the brake force as needed
                wheel_col[i].motorTorque = 0;     // Stop applying motor torque
            }
            else
            {
                // Remove brake torque when moving
                wheel_col[i].brakeTorque = 0;
                wheel_col[i].motorTorque = forwardInput * torque;
            }

            var wheelPosition = transform.position;
            var wheelRotation = transform.rotation;

            wheel_col[i].GetWorldPose(out wheelPosition, out wheelRotation);
            wheels[i].position = wheelPosition;
            wheels[i].rotation = wheelRotation;
        }
    }

    // TEST
    // private void OnMouseDown() {
    //    RestartGame();
    // }
    
    // public void RestartGame()
    // {
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    // }
}
