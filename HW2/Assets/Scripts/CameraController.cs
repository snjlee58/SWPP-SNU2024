using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    // Define three different offsets for three different camera perspectives
    private Vector3 offsetTopDown = new Vector3(0, 15, 0); // Adjust the Y-value to ensure a good top view
    private Vector3 offsetRear = new Vector3(0, 4, -8);    // Standard rear view
    private Vector3 offsetSide = new Vector3(4, 4, 4);     // Side view

    // Start with the rear view as default
    private Vector3 currentOffset;

    // Start is called before the first frame update
    void Start()
    {
        currentOffset = offsetRear; // Initialize with rear view

        // currentOffset = offset1;
    }

    // Update is called once per frame
    void Update()
    {
        // Switch between camera perspectives based on key inputs
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentOffset = offsetTopDown; // Switch to top-down view
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentOffset = offsetRear; // Switch to rear view
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentOffset = offsetSide; // Switch to side view
        }
    }

    void LateUpdate() // Update camera position after the player has moved
    {
         // Set the camera's position relative to the player's position and rotation
        if (currentOffset == offsetTopDown)
        {
            // For top-down view, move the camera above the player
            transform.position = player.transform.position + offsetTopDown;
            
            Vector3 playerRotation = player.transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(90f, playerRotation.y, 0f); // Lock the camera to look down but rotate around the Y-axis
        }
        else
        {
            // For rear and side views, follow the player's position and rotation
            transform.position = player.transform.position + player.transform.rotation * currentOffset;

            // If it's the rear view, keep the camera looking forward
            if (currentOffset == offsetRear)
            {
                transform.LookAt(player.transform.position);
            }
            else if (currentOffset == offsetSide)
            {
                // Adjust the camera's rotation for the side view to look at the player
                transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
            }
        }
    }
}
