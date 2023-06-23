using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ScreenPrinter : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI result; // Victory or Defeat

    [SerializeField]
    private TextMeshProUGUI message; // All enemies defeated or You died

    [SerializeField]
    private TextMeshProUGUI summary; // Stage summary

    [SerializeField]
    private TextMeshProUGUI stage; // Next stage or Restart stage

    [SerializeField]
    private Button stageButton;

    private int stageNumber;
    private GameLogic gameLogic;
    private StageManager stageManager;

    private void Awake() {
        stageNumber = SceneManager.GetActiveScene().buildIndex;
        gameLogic = GameObject.Find("GameManager").GetComponent<GameLogic>();
        stageManager = gameLogic.GetComponent<StageManager>();
    }

    public void Result(string res) {
        gameObject.SetActive(true);
        result.text = res;
        summary.text = gameLogic.StageSummary();
        switch (res) {
            case "VICTORY":
                message.text = "All enemies killed!";
                stage.text = "NEXT";
                summary.text = "Stage " + stageNumber + " cleared!\n" + summary.text;
                stageButton.onClick.AddListener(stageManager.Next);
                break;
            case "DEFEAT":
                message.text = "You died...";
                stage.text = "RESTART";
                summary.text = "Stage " + stageNumber + " failed...\n" + summary.text;
                stageButton.onClick.AddListener(stageManager.Restart);
                break;
        }
    }
}
