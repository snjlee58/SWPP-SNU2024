using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameSceneManager gameSceneManager; // Reference to GameSceneManager
    public UIManager uiManager; // Reference to UIManager
    public List<Wave> waves;
    private int currentWaveIndex = 0;
    private int enemiesDestroyedInCurrentStage = 0;
    private bool isSpawningWave = false;
    private Vector3[] spawnPoints = new Vector3[]
    {
        new Vector3(-8.0f, 0f, 0.4f),
        new Vector3(-9.0f, 0f, 0.4f),
        new Vector3(-10.0f, 0f, 0.4f),
    };

    void Start()
    {
    }

    public void StartNextStage() {
        // Reset number of enemies killed
        enemiesDestroyedInCurrentStage = 0;

        // Start spawning enemies
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
    }
    public void OnEnemyDeath() {
        enemiesDestroyedInCurrentStage++;

        if (gameSceneManager != null) {
            gameSceneManager.AddMoney();
        }

        // Check if stage is complete (all enemies destroyed)
        CheckStageCompletion();
    }

    private void CheckStageCompletion()
    {
        // Check if all enemies in the current wave are defeated
        if (enemiesDestroyedInCurrentStage >= waves[currentWaveIndex - 1].enemyCount)
        {
            if (currentWaveIndex >= waves.Count)
            {
                // All waves are complete, show game clear
                if (uiManager != null)
                {
                    uiManager.ShowGameClear();
                }
            }
            else
            {
                // The current wave is complete, move to the next stage
                if (gameSceneManager != null)
                {
                    gameSceneManager.EndStage();
                    uiManager.UpdateStage(currentWaveIndex + 1); // Update stage number in UI
                }
            }
        }
    }

}
