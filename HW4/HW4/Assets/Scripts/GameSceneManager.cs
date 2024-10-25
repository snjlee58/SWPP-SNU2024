using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private bool isPaused = false;

    public void StartStage() {
        
    }

    public void TogglePause() {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
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
