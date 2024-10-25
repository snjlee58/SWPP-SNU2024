using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    private Transform shootPoint;
    private float detectionRange = 10f;
    private GameObject currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         DetectEnemiesInRange();
        if (currentTarget)
        {
            LookAtTarget();
            ThrowProjectile();
        }
        
    }

    void DetectEnemiesInRange() {
        // Detect all enemies within range and set the nearest one as target
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider hit in hits) {
            if (hit.CompareTag("Enemy")) {
                currentTarget = hit.gameObject;
                break;
            }
        }
    }

    void LookAtTarget()
    {
        if (currentTarget != null)
        {
            Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void ThrowProjectile()
    {
        // Instantiate projectile and set its target
        // GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        // Projectile projectileScript = projectile.GetComponent<Projectile>();
        // projectileScript.SetTarget(currentTarget);
    }
}
