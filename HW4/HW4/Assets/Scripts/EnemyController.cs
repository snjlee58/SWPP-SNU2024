using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = 2.0f;
    private Animator enemyAnimator;

    public int health = 3;
    // public UpgradeController upgradeController;


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
        // upgradeController.money += 1;
        Destroy(gameObject);
    }
}
