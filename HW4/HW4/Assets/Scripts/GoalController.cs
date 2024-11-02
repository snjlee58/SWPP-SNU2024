using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public GameSceneManager gameSceneManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method triggered when another collider enters the goal's collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an enemy
        if (other.CompareTag("Enemy"))
        {
            // Trigger Game Over
            if (gameSceneManager != null)
            {
                gameSceneManager.LoseLife();
                gameSceneManager.GameOver();
            }

            // Destroy the chicken (goal) object
            Destroy(gameObject);
        }
    }
}
