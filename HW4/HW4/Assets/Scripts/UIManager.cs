using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    StageManager stageManager;

    public TextMeshProUGUI stageText;
    public TextMeshProUGUI moneyText;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI gameClearText;

    public Button upgradeButton;

    public void UpdateStage(int _stage) {
        stageText.text = "Stage: " + _stage;
    }

    public void UpdateMoney(int _money) {
        moneyText.text = "Money: " + _money;
    }

    public void ShowGameOver() {
        gameOverText.gameObject.SetActive(true);
        upgradeButton.gameObject.SetActive(false);
    }

    public void ShowGameClear() {
        gameClearText.gameObject.SetActive(true);
        upgradeButton.gameObject.SetActive(false);
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
