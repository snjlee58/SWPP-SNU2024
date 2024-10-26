using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 10f;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTarget(GameObject newTarget) {
        target = newTarget;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
      
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
   {
       // Check if the collider belongs to the target
        if (other.gameObject == target) {
            target.GetComponent<EnemyController>().TakeDamage(1); // Apply damage to the target
            Destroy(gameObject); // Destroy the projectile
        }
   }
}
