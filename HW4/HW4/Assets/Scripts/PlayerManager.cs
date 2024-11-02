using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject originalFarmerPrefab;
    public GameObject upgradedFarmerPrefab;
    private GameObject currentFarmer;

    public bool isUpgraded = false;

    private Vector3 spawnPosition = new Vector3(1.50f, 0f, -3.38f);

    private PlayerController playerController; // Reference to the PlayerController script

    void Start()
    {
        // Instantiate the original farmer at the start and get the PlayerController
        currentFarmer = Instantiate(originalFarmerPrefab,  spawnPosition, transform.rotation);
        playerController = currentFarmer.GetComponent<PlayerController>();

        // Pass the currentFarmer to PlayerController
        playerController.SetCurrentFarmer(currentFarmer);
    }

    public void UpgradeFarmer()
    {
        if (currentFarmer != null)
        {
            // Destroy the current farmer GameObject
            Destroy(currentFarmer);

            // Instantiate the upgraded farmer at the same position and rotation
            currentFarmer = Instantiate(upgradedFarmerPrefab, spawnPosition, transform.rotation);

            // Update the PlayerController reference and pass the new farmer
            playerController = currentFarmer.GetComponent<PlayerController>();
            playerController.SetCurrentFarmer(currentFarmer);

            isUpgraded = true;
        }
    }

    public void OnGameOver() {
        playerController.StopThrowingProjectiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
