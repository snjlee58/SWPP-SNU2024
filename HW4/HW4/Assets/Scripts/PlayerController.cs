using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    private float detectionRange = 7.0f;
    private GameObject currentTarget = null;

    public float fireRate = 0.1f; // Time in seconds between each shot
    private bool isFiring = false;


    // Start is called before the first frame update
     void Start()
    {
        InvokeRepeating("DetectEnemiesInRange", 0f, 0.05f); // Check for enemies within range
    }

    void Update() {
        if (currentTarget) {
            LookAtTarget(); // Ensure the Farmer looks at the target
        }
    }

    void DetectEnemiesInRange() {
        if (currentTarget == null) // Only look for new targets if there is no current target
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    currentTarget = hit.gameObject;
                    StartThrowingProjectiles();
                    break; // Target the first enemy found
                }
            }
        } 
        else {
             // Check if the current target is still alive
            if (!currentTarget.activeInHierarchy) // Check if the enemy is destroyed or inactive
            {
                StopThrowingProjectiles();
                currentTarget = null;
            }
        }
    }

    void StartThrowingProjectiles()
    {
        if (!isFiring) { // Ensure firing starts only once
            isFiring = true;
            InvokeRepeating("ThrowProjectile", 0f, fireRate);
        }
    }

    void StopThrowingProjectiles()
    {
        if (isFiring) // Check to ensure firing stops properly
        {
            isFiring = false;
            CancelInvoke("ThrowProjectile");
        }
    }

    void ThrowProjectile()
    {
        if ( projectilePrefab != null && currentTarget != null) {
            // Instantiate projectile and set its target
            GameObject projectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            ProjectileController projectileScript = projectile.GetComponent<ProjectileController>();
            projectileScript.SetTarget(currentTarget);
        }
    }

    void LookAtTarget() {
        if (currentTarget != null) {
            Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
