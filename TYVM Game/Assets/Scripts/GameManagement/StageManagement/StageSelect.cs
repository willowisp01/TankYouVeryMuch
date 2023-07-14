using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour {

    [SerializeField]
    private Button main;
    private List<Button> stageButtons;
    private StageManager stageManager;

    [SerializeField]
    private StagesUnlocked stagesUnlocked;

    private void Awake() {
        stageButtons = FindObjectsOfType<Button>().ToList();
        stageButtons.Remove(main);
        stageManager = GetComponent<StageManager>();
        foreach (Button stageButton in stageButtons) {
            string stageNum = stageButton.name;
            if (stagesUnlocked.IsUnlocked(stageNum)) {
                stageButton.interactable = true;
                stageButton.onClick.AddListener(() => stageManager.SelectStage(stageNum));
            }
        }
    }
}
