using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int collectedCoins = 0;
    public GameObject barrier;
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

        if (collectedCoins >= 8)
        {
            // Move barrier up if all coins are collected
            StartCoroutine(MoveBarrierUp());
        }
    }

    // Coroutine to move the barrier up 
    private IEnumerator MoveBarrierUp()
    {
        // Move the barrier until it reaches the target height
        Vector3 targetPosition = new Vector3(barrier.transform.position.x, barrierMoveHeight, barrier.transform.position.z);

        while (Vector3.Distance(barrier.transform.position, targetPosition) > 0.01f)
        {
            // Move barrier slowly towards the target height
            barrier.transform.position = Vector3.MoveTowards(barrier.transform.position, targetPosition, barrierMoveSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame before continuing the loop
        }

        // Set new position of barrer
        barrier.transform.position = targetPosition;
    }
}
