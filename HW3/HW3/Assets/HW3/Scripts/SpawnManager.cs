using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] bombs;
    private Vector3[] bombPositions;
    private Quaternion[] bombRotations;
    public GameObject bombPrefab; 
    private float respawnDelay = 2.0f;
    private bool explodeEvenGroup = true; // To alternate between even and odd explosions


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

        // Start alternating explosions
        if (bombs.Length > 0) {
            InvokeRepeating("ExplodeGroup", 3.0f, respawnDelay + 3.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {     
    }

    // Function to alternate between exploding even and odd bombs
    void ExplodeGroup() {
        if (explodeEvenGroup) {
            ExplodeBombs(true);
        } else {
            ExplodeBombs(false); 
        }

        explodeEvenGroup = !explodeEvenGroup;
    }

    // Function to explode bombs based on even/odd indices
    void ExplodeBombs(bool isEven)
    {
        for (int i = 0; i < bombs.Length; i++) {
            // Check if it's an even or odd bomb based on the index
            if (isEven && i % 2 == 0 || !isEven && i % 2 != 0) {
                GameObject selectedBomb = bombs[i];
                if (selectedBomb != null) {
                    Bomb bombScript = selectedBomb.GetComponent<Bomb>();
                    bombScript.Explode();

                    // Respawn bomb after delay
                    StartCoroutine(RespawnBomb(i));
                }
            }
        }
    }

    // Coroutine to respawn the bomb after a delay
    IEnumerator RespawnBomb(int index) {
        yield return new WaitForSeconds(respawnDelay);
        GameObject newBomb = Instantiate(bombPrefab, bombPositions[index], bombRotations[index]);
        bombs[index] = newBomb;
    }
}
