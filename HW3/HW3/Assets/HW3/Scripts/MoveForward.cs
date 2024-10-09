using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    // On collision with a wall, reverse direction
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object collided with a wall (using a tag or the cube's name)
        if (collision.gameObject.CompareTag("Wall"))  // Ensure your wall object has the "Wall" tag
        {
            // Reverse the direction by rotating 180 degrees around the Y-axis
            transform.Rotate(0, 180, 0);

            // Alternatively, reverse the movement by changing the speed sign (use this if you want them to go backward, not just flip):
            // speed = -speed;
        }
    }
}
