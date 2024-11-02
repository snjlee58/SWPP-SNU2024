using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public StageManager stageManager; // Reference to StageManager
    public PlayerManager playerManager; // Reference to PlayerManager
    UIManager uiManager;

    // Buttons
    public Button startButton;
    public Button pauseButton;

    public Button upgradeButton;

    // Variables to track game status
    private bool isGameOver = false;
    private bool isStageActive = false;
    private bool isPaused = false;
    private int life = 1;
    private int money = 0;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = transform.Find("UIManager").GetComponent<UIManager>();
    }
    
    // Start Stage Button
    /*
        - 해당 버튼을 눌러야만 스테이지를 시작 할 수 있음
        - 스테이지 진행 중과 게임 오버 상황에서는 동작하지 않음
        - 스테이지가 시작되면 빨간색으로 변하고 비활성화
        - 스테이지가 끝나면 다시 검은색으로 변하고 재활성화
    */
    public void OnStartButtonPressed() {
        if (!isStageActive && !isGameOver) {
            StartStage();
        }
    }
    public void StartStage() {
        isStageActive = true;
        startButton.image.color = Color.red;

        // Call StartNextStage on StageManager to initiate the stage
        if (stageManager != null) {
            stageManager.StartNextStage();
        }
    }

    public void EndStage() {
        isStageActive = false;
        if (startButton != null) {
            startButton.image.color = Color.white;
        }
    }

    // Pause Button 
    /* 
        - 게임 플레이 중 누르면 게임이 일시정지
        - 게임 오버 상황에서는 눌러도 동작하지 않음
    */
    public void OnPauseButtonPressed() {
        if (!isGameOver) {
            if (isPaused) {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }
    }

    private void PauseGame() {
        Time.timeScale = 0; // Freezes the game
        isPaused = true;

    }

    private void ResumeGame() {
        Time.timeScale = 1; // Resumes the game
        isPaused = false;
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoseLife() {
        life--;
        uiManager.UpdateLife(life);
    }

    // Method to add money
    public void AddMoney() {
        money++;
        uiManager.UpdateMoney(money);

        // Show upgrade button if money reaches 3
        if (money >= 3) {
            upgradeButton.gameObject.SetActive(true);
        }
    }

    // Method to handle upgrade button click
    public void UpgradePlayer()
    {
        if (!isGameOver && money >= 3 && !playerManager.isUpgraded) {
            money -= 3;
            upgradeButton.gameObject.SetActive(false);

            // Update money in UI
            uiManager.UpdateMoney(money);

            playerManager.UpgradeFarmer();

            // Here, you could change the player prefab or any other upgrades
            Debug.Log("Player Upgraded!"); // DEBUG
        }
    }

    public void GameOver()
    {
        if (isGameOver) return; // Prevent repeated game-over logic

        isGameOver = true;

        EndStage(); // Turn off Start Button

        if (uiManager != null)
        {
            playerManager.OnGameOver();
            uiManager.ShowGameOver();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
