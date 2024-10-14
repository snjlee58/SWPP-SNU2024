using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMoveForward : MonoBehaviour
{
    public float speed = 40.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
        // Destroy weapon if it hits wall
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy")) {
            Destroy(gameObject);
            // Destroy animal on collision
            Destroy(collision.gameObject);
        }
    }
}