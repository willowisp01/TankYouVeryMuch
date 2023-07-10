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
    private GameEventListener victoryListener;

    [SerializeField]
    private GameEventListener defeatListener;

    private int stageNumber;

    private void Awake() {
        victoryListener.nextEvent.AddListener(() => Result("VICTORY"));
        defeatListener.nextEvent.AddListener(() => Result("DEFEAT"));
        stageNumber = SceneManager.GetActiveScene().buildIndex;
    }

    private void Result(string res) {
        transform.Find("Screen").gameObject.SetActive(true);
        result.text = res;
        summary.text = GameObject.Find("GameManager").GetComponent<GameLogic>().StageSummary();
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
