using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Player animator
    private Animator playerAnimator;
    private GameObject currentFarmer;

    // Enemy tracking
    private float detectionRange = 7.0f;
    private List<GameObject> enemyQueue = new List<GameObject>();
    private GameObject currentTarget = null;

    // Projectile firing
    public GameObject projectilePrefab;
    public float fireRate = 0.8f; // Time in seconds between each shot
    private bool isFiring = false;
    private int shotsFired = 0; // Tracks the number of shots fired
    private int shotsToFire = 0; // Number of shots to fire based on enemy's health


    // Start is called before the first frame update
     void Start()
    {
        // Initialize the animator when the farmer is set
        if (currentFarmer != null)
        {
            playerAnimator = currentFarmer.GetComponent<Animator>();
        }
    }

    void Update() {
        ContinuousEnemyDetection();
        ManageTargetAndFiring();

        if (currentTarget) {
            LookAtTarget();
        }
    }

    public void SetCurrentFarmer(GameObject farmer)
    {
        currentFarmer = farmer;
        playerAnimator = currentFarmer.GetComponent<Animator>();
    }

     void ContinuousEnemyDetection()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy") && !enemyQueue.Contains(hit.gameObject))
            {
                enemyQueue.Add(hit.gameObject);
            }
        }
    }

    void ManageTargetAndFiring()
    {
        // If there is no current target, select the next one in the queue
        if (currentTarget == null && enemyQueue.Count > 0)
        {
            currentTarget = enemyQueue[0];
            enemyQueue.RemoveAt(0);

            if (currentTarget != null)
            {
                EnemyController enemy = currentTarget.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    shotsToFire = enemy.health; // Set the number of shots based on enemy's health
                    shotsFired = 0; // Reset shots fired count
                    StartThrowingProjectiles();
                }
            }
        }

        // If the current target is destroyed, stop firing and clear the target
        if (currentTarget != null && !currentTarget.activeInHierarchy)
        {
            StopThrowingProjectiles();
            currentTarget = null;
        }
    }

    void StartThrowingProjectiles()
    {
        if (!isFiring) { // Ensure firing starts only once
            isFiring = true;
            InvokeRepeating("ThrowProjectile", 0f, fireRate);
        }
    }

    public void StopThrowingProjectiles()
    {
        if (isFiring) // Check to ensure firing stops properly
        {
            isFiring = false;
            CancelInvoke("ThrowProjectile");
            playerAnimator.SetInteger("Animation_int", 0); // Set to idle animation state
        }
    }

    void ThrowProjectile()
    {
        if (currentTarget != null) {
            playerAnimator.speed = fireRate > 0.5f ? 1.5f : 2.9f; 
            playerAnimator.SetInteger("Animation_int", 3); // Set to throwing animation state

            // Instantiate projectile and set its target
            GameObject projectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
            ProjectileController projectileScript = projectile.GetComponent<ProjectileController>();
            projectileScript.SetTarget(currentTarget);

            shotsFired++;
            if (shotsFired >= shotsToFire) // Check number of shots the player has fired
            {
                StopThrowingProjectiles();
            }
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
