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

    private int stageNumber;
    private GameLogic gameLogic;

    private void Awake() {
        stageNumber = SceneManager.GetActiveScene().buildIndex;
        gameLogic = GameObject.Find("GameManager").GetComponent<GameLogic>();
    }

    public void Result(string res) {
        gameObject.SetActive(true);
        result.text = res;
        summary.text = gameLogic.StageSummary();
        switch (res) {
            case "VICTORY":
                message.text = "All enemies killed!";
                summary.text = "Stage " + stageNumber + " cleared!\n" + summary.text;
                break;
            case "DEFEAT":
                message.text = "You died...";
                summary.text = "Stage " + stageNumber + " failed...\n" + summary.text;
                break;
        }
    }
}
