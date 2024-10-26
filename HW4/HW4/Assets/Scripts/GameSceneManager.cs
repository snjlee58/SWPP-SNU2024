using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    public Button startButton;
    public StageManager stageManager; // Reference to StageManager
    private bool isStageActive = false;
    private bool isPaused = false;
    
    // Start Stage Button
    /*
        - 해당 버튼을 눌러야만 스테이지를 시작 할 수 있음
        - 스테이지 진행 중과 게임 오버 상황에서는 동작하지 않음
        - 스테이지가 시작되면 빨간색으로 변하고 비활성화
        - 스테이지가 끝나면 다시 검은색으로 변하고 재활성화
    */
    public void OnStartButtonPressed() {
        if (!isStageActive) {
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
        startButton.image.color = Color.white;
    }

    // Pause Button 
    /* 
        - 게임 플레이 중 누르면 게임이 일시정지
        - 게임 오버 상황에서는 눌러도 동작하지 않음
    */
    public void TogglePause() {
        if (isPaused) {
            ResumeGame();
        }
        else {
            PauseGame();
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

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
