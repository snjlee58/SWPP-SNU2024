using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] bombs;
    private Vector3[] bombPositions;
    private Quaternion[] bombRotations;
    public GameObject bombPrefab; // For respawning bombs
    private float respawnDelay = 3.0f;
    private int bombIndex;

    // Start is called before the first frame update
    void Start()
    {
         // Store bomb positions and rotations
        bombPositions = new Vector3[bombs.Length];
        bombRotations = new Quaternion[bombs.Length];
        for (int i = 0; i < bombs.Length; i++) {
            if (bombs[i] != null) {
                bombPositions[i] = bombs[i].transform.position;
                bombRotations[i] = bombs[i].transform.rotation;
            }
        }

        if (bombs.Length > 0) {
            InvokeRepeating("ExplodeRandomBomb", 2.0f, respawnDelay + 2.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {     
    }

    void ExplodeRandomBomb() {
        bombIndex = Random.Range(0, bombs.Length);
        GameObject selectedBomb = bombs[bombIndex];

        if (selectedBomb != null) {
            Bomb bombScript = selectedBomb.GetComponent<Bomb>();
            bombScript.Explode();

            Invoke("RespawnBomb", respawnDelay);
        }
    }
    void RespawnBomb()
    {
        // Instantiate a new bomb at the original position
        GameObject newBomb = Instantiate(bombPrefab, bombPositions[bombIndex], bombRotations[bombIndex]);
        bombs[bombIndex] = newBomb;
    }
}
