using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    private float detectionRange = 6.0f;
    private GameObject currentTarget;

    public float fireRate = 0.3f; // Time in seconds between each shot


    // Start is called before the first frame update
     void Start()
    {
        InvokeRepeating("DetectEnemiesInRange", 0f, 0.1f); // Check for enemies within range
    }

    void Update() {
        if (currentTarget) {
            LookAtTarget(); // Ensure the Farmer looks at the target
        }
    }

    void DetectEnemiesInRange() {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                if (currentTarget == null) // Only start firing if there was no target before
                {
                    currentTarget = hit.gameObject;
                    StartThrowingProjectiles();
                }
                return;
            }
        }

        // If no enemies are detected, stop firing projectiles
        if (currentTarget != null) {
            currentTarget = null;
            StopThrowingProjectiles();
        }
    }

    void StartThrowingProjectiles()
    {
        InvokeRepeating("ThrowProjectile", 0f, fireRate);
    }

    void StopThrowingProjectiles()
    {
        CancelInvoke("ThrowProjectile");
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
