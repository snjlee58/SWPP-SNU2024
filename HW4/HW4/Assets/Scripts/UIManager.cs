using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    StageManager stageManager;

    public TextMeshProUGUI stageText;

    public void UpdateStage(int _stage) {
        stageText.text = "Stage: " + _stage;
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
