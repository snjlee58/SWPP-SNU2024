using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    Vector3 initialBackgroundPos;
    Vector3 currentBackgroundPos;
    float resetPosX = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        initialBackgroundPos = transform.position;
        // Width of background;
        resetPosX = GetComponent<BoxCollider>().size.x / 2.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        currentBackgroundPos = transform.position;
        if (currentBackgroundPos.x - initialBackgroundPos.x < - resetPosX) {
            transform.position = initialBackgroundPos;
        }
        
    }
}
