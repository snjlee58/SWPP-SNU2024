using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameSceneManager gameSceneManager; // Reference to GameSceneManager
    public UIManager uiManager;
    public List<Wave> waves;
    private int currentWaveIndex = 0;
    private bool isSpawningWave = false;
    private Vector3[] spawnPoints = new Vector3[]
    {
        new Vector3(-6.0f, 0f, 0.4f),
        new Vector3(-7.0f, 0f, 0.4f),
        new Vector3(-8.0f, 0f, 0.4f),
    };

    void Start()
    {
    }

    public void StartNextStage() {
        Debug.Log("starting stage" + currentWaveIndex); // DEBUG
        if (currentWaveIndex < waves.Count) {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
        }
    }

    private IEnumerator SpawnWave(Wave wave) {
        isSpawningWave = true;

        for (int i = 0; i < wave.enemyCount; i++) {
            // Instantiate enemy
            GameObject enemy = Instantiate(wave.enemyPrefab, spawnPoints[i], wave.enemyPrefab.transform.rotation);
            // Set reference to StageManager in EnemyController
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null) {
                enemyController.stageManager = this;
            }

            yield return new WaitForSeconds(wave.spawnDelay); // Wait before spawning the next enemy
        }

        isSpawningWave = false;
        currentWaveIndex++; // Move to the next wave

        // After spawning all enemies in this wave, check for completion
        StartCoroutine(CheckWaveCompletion(wave));
    }
    public void OnEnemyDeath() {
        if (gameSceneManager != null) {
            gameSceneManager.AddMoney();
        }
    }

    private IEnumerator CheckWaveCompletion(Wave wave) {
        // Wait until all enemies in the wave are defeated
        while (!wave.AllEnemiesDefeated()) {
            yield return new WaitForSeconds(1f); // Check every second
        }

        if (gameSceneManager != null) {
            Debug.Log("Stage completed!"); // DEBUG
            gameSceneManager.EndStage();
            uiManager.UpdateStage(currentWaveIndex+1); // Update stage number in UI
        }
    }
}
