using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int collectedCoins = 0;
    public GameObject[] barriers; 
    public int[] coinsPerLevel; 
    private int currentBarrier = 0;
    private float barrierMoveSpeed = 5.0f;
    private float barrierMoveHeight = 40.0f;

    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {  
    }

    // When a coin is collected
    public void CoinCollected()
    {
        collectedCoins++;

        // Check if the player has collected all the coins for the current barrier level
        if (collectedCoins >= coinsPerLevel[currentBarrier])
        {
            // Open the barrier for the current level
            StartCoroutine(MoveBarrierUp(barriers[currentBarrier]));

            // Move to the next barrier level
            currentBarrier++;
            collectedCoins = 0;

            // Print message when all levels are completed
            if (currentBarrier >= barriers.Length)
            {
                Debug.Log("All levels completed!");
            }
        }
    }

    // Coroutine to move the barrier up 
    private IEnumerator MoveBarrierUp(GameObject barrier)
    {
        // Move the barrier until it reaches the target height
        Vector3 targetPosition = new Vector3(barrier.transform.position.x, barrierMoveHeight, barrier.transform.position.z);

        while (Vector3.Distance(barrier.transform.position, targetPosition) > 0.01f)
        {
            barrier.transform.position = Vector3.MoveTowards(barrier.transform.position, targetPosition, barrierMoveSpeed * Time.deltaTime);
            yield return null; 
        }

        barrier.transform.position = targetPosition;
    }
}
