using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = 1.0f;
    private Animator enemyAnimator;

    public int health = 3;
    public StageManager stageManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyAnimator.SetFloat("Speed_f", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);  
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        // Destroy enemy object
        Destroy(gameObject);
        
        // Notify StageManager of enemy death
        if (stageManager != null) {
            stageManager.OnEnemyDeath(); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the enemy collides with the chicken
        if (other.CompareTag("Goal"))
        {
            // Destroy the chicken
            Destroy(other.gameObject);
        }
    }
}
