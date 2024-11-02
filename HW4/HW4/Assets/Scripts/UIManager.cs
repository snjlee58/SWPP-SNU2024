using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    StageManager stageManager;

    public TextMeshProUGUI stageText;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI moneyText;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI gameClearText;

    public Button upgradeButton;

    public void UpdateStage(int _stage) {
        stageText.text = "Stage: " + _stage;
    }

    public void UpdateLife(int _life) {
        lifeText.text = "Life: " + _life;
    }

    public void UpdateMoney(int _money) {
        moneyText.text = "Money: " + _money;
    }

    public void ShowGameOver() {
        gameOverText.gameObject.SetActive(true);
    }

    public void ShowGameClear() {
        gameClearText.gameObject.SetActive(true);
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
